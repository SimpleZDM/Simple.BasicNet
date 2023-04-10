using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Simple.BasicNet.Core.Atrributes;
using Simple.BasicNet.Core.Configuration;
using Simple.BasicNet.Core.Handle;
using Simple.BasicNet.Core.Schedule;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Net
 * 接口名称 Host
 * 开发人员：11920
 * 创建时间：2023/4/6 15:33:15
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Net
{
    public class Host: IHost
	{
		IContainer container;
        ServiceConfigution serviceConfigution;
		IPretty prettyCore { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public static IHost BuilderHost()
		{
			return new Host();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public IHost Start()
		{
			prettyCore = container.GetService<IPretty>();
			prettyCore.Start();
			return this;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ConfigurationPath"></param>
		/// <returns></returns>
		public IHost Start(string ConfigurationPath)
		{
			ConfigurationBulder configurationBulder = new ConfigurationBulder(ConfigurationPath);
			serviceConfigution = configurationBulder.GetConfigution();
			return Start();

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="SetConfigution"></param>
		/// <returns></returns>
		public IHost Start(Action<ServiceConfigution>SetConfigution)
		{
			serviceConfigution =container.GetService<ServiceConfigution>();
			if (SetConfigution!=null)
			{
				SetConfigution.Invoke(serviceConfigution);
			}
			return Start();
		}
		/// <summary>
		/// 替换容器之后需要重新注册
		/// </summary>
		/// <returns></returns>
		public IHost ReplaceContainer(IContainer container)
		{
			container=Container.SetContainer(container);
			Initalization();
			return this;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public IHost Regster(Action<IContainer> action)
		{
			if (action==null)
			{
				throw new ArgumentNullException(nameof(action));
			}
			action.Invoke(container);
			return this;
		}
		/// <summary>
		/// 注册任务然后启动任务
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// 
		public IHost RegisterSchedule(Action<IScheduleManager> action)
		{
			if (action==null)
			{
				throw new ArgumentNullException(nameof(action));
			}
			var scheduleManager = container.GetService<IScheduleManager>();
			action.Invoke(scheduleManager);
			scheduleManager.Start();
			return this;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TMessageHandle"></typeparam>
		/// <returns></returns>
		public IHost ConfigutionMessageHandle<TMessageHandle>() where TMessageHandle : IMessageHandle
		{   
			container.RegisterSingleton<IMessageHandle, TMessageHandle>().Autowird();
			return this;
		}
		/// <summary>
		/// 
		/// </summary>
		private Host()
		{
			container = Container.GetContainer();
			Initalization();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="container"></param>
		/// <returns></returns>
		private void Initalization()
		{
			container.Register<HandleContext>().Autowird();
			container.RegisterSingleton<ServiceConfigution>().Autowird();
			container.RegisterSingleton<IMessageHandle,MessageHandle>().Autowird();
            container.RegisterSingleton<IClientManager, ClientManager>().Autowird();
			container.RegisterSingleton<IScheduleManager, ScheduleManager>().Autowird();
			container.RegisterSingleton<IPretty, Pretty>().Autowird();
			ConsoleLog.DEBUGLOG("容器初始化成功!");
		}
	}
}
