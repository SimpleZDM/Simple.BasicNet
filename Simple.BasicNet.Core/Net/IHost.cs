using Simple.BasicNet.Core.Configuration;
using Simple.BasicNet.Core.Handle;
using Simple.BasicNet.Core.Schedule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Net
 * 接口名称 IHost
 * 开发人员：11920
 * 创建时间：2023/4/7 17:29:14
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Net
{
	public interface IHost
	{
		public IHost Start();
		public IHost Start(string ConfigurationPath);
		public IHost Start(Action<ServiceConfigution> SetConfigution);
		public IHost ReplaceContainer(IContainer container);
		public IHost Regster(Action<IContainer> action);
		public IHost RegisterSchedule(Action<IScheduleManager> action);
		public IHost ConfigutionMessageHandle<TMessageHandle>() where TMessageHandle : IMessageHandle;
	}
}
