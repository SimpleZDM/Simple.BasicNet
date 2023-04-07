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
    public class Host
	{
		Socket serviceSocket;
		ServiceConfigution serviceConfigution;
		ClientManager clientManager;
		IContainer container;

		public Host() 
		{

		    serviceConfigution = new ServiceConfigution();
			container=Container.BuilderContainer();
			clientManager = new ClientManager(container);
		}
		public Host ConfigutionMessageHandle<TMessageHandle>()where TMessageHandle:IMessageHandle
		{
			//type
			container.Register<IMessageHandle,TMessageHandle>();
			return this;
		}
		public Host ConfigutionContainer(IContainer container)
		{
			//type
			this.container = container;
			return this;
		}

		public  void Start(Action<ServiceConfigution>SetConfigution)
		{
			if (SetConfigution!=null)
			{
				SetConfigution.Invoke(serviceConfigution);
			}
			Start();
		}

		public void Start(string ConfigurationPath)
		{
			ConfigurationBulder configurationBulder = new ConfigurationBulder(ConfigurationPath);
			serviceConfigution= configurationBulder.GetConfigution();
			Start();

		}
		public void Start()
		{
			IPEndPoint endPoint = new IPEndPoint(serviceConfigution.GetIpAddress(), serviceConfigution.Port);
			serviceSocket = new Socket(endPoint.AddressFamily, serviceConfigution.SocketType, serviceConfigution.ProtocolType);
			ConsoleLog.DEBUGLOG("服务器启动成功!");
			ConsoleLog.DEBUGLOG($"{endPoint.Address.ToString()}:{serviceConfigution.Port}");
			InitalizationContainer();
			serviceSocket.Bind(endPoint);
			serviceSocket.Listen(serviceConfigution.Backlog);
			ServiceAccept();
		}
		public void InitalizationContainer()
		{
			container.RegisterSingleton(serviceConfigution);
			container.RegisterSingleton<IMessageHandle,MessageHandle>();
			container.Register<HandleContext>();
			ConsoleLog.DEBUGLOG("容器初始化成功!");
		}

		public void ServiceAccept()
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
