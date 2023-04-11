using Simple.BasicNet.Core.Atrributes;
using Simple.BasicNet.Core.Net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Net
 * 接口名称 MessageHandle
 * 开发人员：11920
 * 创建时间：2023/4/6 16:29:04
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Handle
{
    public class MessageHandle : IMessageHandle
    {
        IContainer container;

        List<HandleLabel> handleLabels;
        ILogger logger;
        public MessageHandle(IContainer container)
        {
            this.container = container;
            handleLabels = new List<HandleLabel>();
            logger = container.GetService<ILogger>();
			Initalization();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void AnalysisHandle(HandleContext context)
        {
            #region hader
            context.HeaderBuffer = new byte[2];
            Array.Copy(context.ReceiveBuffer, 0, context.HeaderBuffer, 0, 2);
            #endregion

            #region tail
            context.TailBuffer = new byte[2];
            Array.Copy(context.ReceiveBuffer, context.Length - 2, context.HeaderBuffer, 0, 2);
            #endregion

            #region Code
            context.CommandID = context.ReceiveBuffer[2];
            #endregion

            #region extand data verify 

            #endregion
        }

        /// <summary>
        /// 后续逻辑处理
        /// </summary>
        /// <param name="context"></param>
        public void Handle(HandleContext context)
        {
            if (handleLabels.Where(label => label.IsRange(context.CommandID)).Any())
            {
                var label = handleLabels.FirstOrDefault(label => label.IsRange(context.CommandID));
                var handle = container.GetService(label.HandType);

                ((BaseHandle)handle).SetContext(context);
				
				foreach (var method in label.HandType.GetMethods().Where(m => m.IsDefined(typeof(MessageNumberAttribute))))
                {
                    MessageNumberAttribute NumberAttr = method.GetCustomAttribute<MessageNumberAttribute>();

                    if (NumberAttr.Number == context.CommandID)
                    {
                        method.Invoke(handle, new object[] { });
                    }
                }
            }
            else
            {
				logger.Error($"消息号{context.CommandID}不存在处理的方法,请检查!");
            }
        }

        public void Initalization()
        {
            container.Register<HandleLabel>();
            #region 
            Type type = typeof(BaseHandle);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembliy in assemblies)
            {
                foreach (var itemType in assembliy.GetTypes())
                {
                    if (itemType.IsAssignableTo(type))
                    {
                        container.Register(itemType);
                        if (itemType.IsDefined(typeof(NumberRangeAttribute), true))
                        {

                            NumberRangeAttribute range = itemType.GetCustomAttribute<NumberRangeAttribute>();
                            var handleLabel = container.GetService<HandleLabel>();
                            handleLabel.MaxNumber = range.MaxMessageNumber;
                            handleLabel.MinNumber = range.MinMessageNumber;
                            handleLabel.HandType = itemType;
                            if (handleLabels.Where(labels => labels.IsRange(range.MinMessageNumber) || labels.IsRange(range.MaxMessageNumber)).Any())
                                throw new Exception("处理类的范围冲突,请检查,确保范围不重叠!");
                            handleLabels.Add(handleLabel);
                        }
                    }
                }
            }

            #endregion

        }
    }
}
