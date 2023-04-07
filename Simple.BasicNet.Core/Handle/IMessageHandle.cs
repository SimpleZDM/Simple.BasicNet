using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.BasicNet.Core.Net;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Net
 * 接口名称 IMessageHandle
 * 开发人员：11920
 * 创建时间：2023/4/6 16:28:34
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Handle
{
    public interface IMessageHandle
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void AnalysisHandle(HandleContext context);

        /// <summary>
        /// 后续逻辑处理
        /// </summary>
        /// <param name="context"></param>
        public void Handle(HandleContext context);
    }
}
