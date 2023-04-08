using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Simple.BasicNet.Core.Atrributes;
using Simple.BasicNet.Core.Configuration;
using Simple.BasicNet.Core.Handle;

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

		[Autowired]
		IPretty prettyCore { get; set; }
		[Autowired]
        IClientManager clientManager { get; set; }
        public static IHost BuilderHost()
		{
			return new Host();
		}
		private Host() 
		{

			container=Container.BuilderContainer();
			InitalizationContainer();
		}
		public IHost ConfigutionMessageHandle<TMessageHandle>()where TMessageHandle:IMessageHandle
		{
			container.RegisterSingleton<IMessageHandle,TMessageHandle>();
			return this;
		}
		public IHost ConfigutionContainer(IContainer container)
		{
			this.container = container;
			return this;
		}

		public  IHost Start(Action<ServiceConfigution>SetConfigution)
		{
			serviceConfigution = container.GetService<ServiceConfigution>();
			if (SetConfigution!=null)
			{
				SetConfigution.Invoke(serviceConfigution);
			}
			return Start();
		}

		public IHost Start(string ConfigurationPath)
		{
			ConfigurationBulder configurationBulder = new ConfigurationBulder(ConfigurationPath);
			serviceConfigution= configurationBulder.GetConfigution();
			return Start();

		}
		public IHost Start()
		{
			prettyCore.Start();
			return this;
		}

		public IHost CheckHeart()
		{
			clientManager.CheckHeart();
			return this;
		}
		private void InitalizationContainer()
		{
			container.Register<HandleContext>();
			container.RegisterSingleton<ServiceConfigution>();
			container.RegisterSingleton<IMessageHandle,MessageHandle>();
			container.RegisterSingleton<IPretty,Pretty>();
            container.RegisterSingleton<IClientManager, ClientManager>();
            ConsoleLog.DEBUGLOG("容器初始化成功!");
		}
	}
}
