using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
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
		Socket serviceSocket;
		ServiceConfigution serviceConfigution;
		IClientManager clientManager;
		IContainer container;
		IPretty prettyCore;
		public static IHost BuilderHost()
		{
			return new Host();
		}
		private Host() 
		{

			container=Container.BuilderContainer();
			InitalizationContainer();
			container.RegisterSingleton<IClientManager, ClientManager>();
			clientManager = container.GetService<IClientManager>();
		}
		public IHost ConfigutionMessageHandle<TMessageHandle>()where TMessageHandle:IMessageHandle
		{
			//type
			container.Register<IMessageHandle,TMessageHandle>();
			return this;
		}
		public IHost ConfigutionContainer(IContainer container)
		{
			//type
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
			IPEndPoint endPoint = new IPEndPoint(serviceConfigution.GetIpAddress(), serviceConfigution.Port);
			serviceSocket = new Socket(endPoint.AddressFamily, serviceConfigution.SocketType, serviceConfigution.ProtocolType);
			ConsoleLog.DEBUGLOG("服务器启动成功!");
			ConsoleLog.DEBUGLOG($"{endPoint.Address.ToString()}:{serviceConfigution.Port}");
			serviceSocket.Bind(endPoint);
			serviceSocket.Listen(serviceConfigution.Backlog);
			ServiceAccept();
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
			ConsoleLog.DEBUGLOG("容器初始化成功!");
		}
		private void ServiceAccept()
		{
				while (true)
				{
					try
					{
						var client = serviceSocket.Accept();
						if (clientManager.AddClient(client))
						{
							ConsoleLog.DEBUGLOG($"{client.RemoteEndPoint.ToString()}链接成功!");
						}
					}
					catch (Exception ex)
					{
						ConsoleLog.DEBUGLOG(ex.Message);
						ConsoleLog.DEBUGLOG(ex.InnerException.Message);
					}

				}
		}
	}
}
