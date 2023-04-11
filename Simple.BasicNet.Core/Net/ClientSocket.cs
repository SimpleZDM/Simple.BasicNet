using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Simple.BasicNet.Core.Handle;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Net
 * 接口名称 ClientSocket
 * 开发人员：11920
 * 创建时间：2023/4/6 15:36:04
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Net
{
    public class ClientSocket: IDisposable
	{
	
		public ClientSocket(IContainer container,Socket socket)
		{
			if (socket==null)
			{
				throw new ArgumentNullException(nameof(socket));
			}
			Client = socket;
			ConnectionID=Guid.NewGuid();
			this.container = container;
			messageHandle = container.GetService<IMessageHandle>();
			clientManager=container.GetService<IClientManager>();
			logger=container.GetService<ILogger>();
			receiveBuffer =new byte[1024*1024];
			Receive();
		}
		public Socket Client { get { return client; }private set { client = value; } }
		public Guid ConnectionID { get { return connectionID; }private set { connectionID = value; } }

		private Socket client;
		private Guid connectionID;
		private byte[] receiveBuffer;
		private IMessageHandle messageHandle;
		private IContainer container;
		private IClientManager clientManager;
		private ILogger logger;

		public void Send(byte[] buffer)
		{
			try
			{
				if (client.Connected)
					client.Send(buffer);
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				if (ex.InnerException != null)
				{
					logger.Error(ex.InnerException.Message);
				}
				Dispose();
			}
		}
		private void Receive()
		{
			HandleContext context = container.GetService<HandleContext>();

			context.SetSend((sendBuffer) => Send(sendBuffer));
			
			//阻塞方法
			Task.Run(() =>
			{
				while (true)
				{
					try
					{
						if (client.Connected)
							break;
						context.Length = client.Receive(receiveBuffer);
						context.ReceiveBuffer=new byte[context.Length];
						Array.Copy(receiveBuffer,context.ReceiveBuffer,context.Length);

						messageHandle.AnalysisHandle(context);

						messageHandle.Handle(context);
					}
					catch (Exception ex)
					{

						logger.Error(ex.Message);
						if (ex.InnerException!=null)
						{
							logger.Error(ex.InnerException.Message);
						}
						Dispose();
						break;
					}
				}
			});

		}

		public void Dispose()
		{
			client.Dispose();
			clientManager.RemoveClient(ConnectionID);
			logger.Info($"{ConnectionID}断开链接!");
		}
	}
}
