using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
			container = new Container();
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

		public  void Start(Action<ServiceConfigution> SetConfigution)
		{
			if (SetConfigution != null)
			{
				SetConfigution.Invoke(serviceConfigution);
			}

			IPEndPoint endPoint = new IPEndPoint(serviceConfigution.GetIpAddress(),serviceConfigution.Port);
		    serviceSocket = new Socket(endPoint.AddressFamily, serviceConfigution.SocketType, serviceConfigution.ProtocolType);
			serviceSocket.Bind(endPoint);
			serviceSocket.Listen(serviceConfigution.Backlog);
			ServiceAccept();
		}

		public void InitalizationContainer()
		{
			container.Register<IMessageHandle,MessageHandle>();
		}

		public void ServiceAccept()
		{
			while (true)
			{
				try
				{
					var client = serviceSocket.Accept();
					clientManager.AddClient(client);
				}
				catch (Exception ex)
				{
#if DEBUG
					Console.WriteLine(ex.Message);
					Console.WriteLine(ex.InnerException.Message);
#endif
				}

			}
		}
	}
}
