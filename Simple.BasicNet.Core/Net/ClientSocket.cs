using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
	public class ClientSocket
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
		}
		public Socket Client { get { return client; } set { client = value; } }
		public Guid ConnectionID { get { return connectionID; } set { connectionID = value; } }
		public IMessageHandle MessageHandle { get { return messageHandle; } set { messageHandle = value; } }

		private Socket client;
		private Guid connectionID;
		private byte[] receiveBuffer;
		private byte[] sendBuffer;
		private int length;
		private IMessageHandle messageHandle;
		private IContainer container;

		public void Send(byte[] sendBuffer)
		{
			//client.Send();
		}
		public void Receive()
		{
			length=client.Receive(receiveBuffer);
		}
	}
}
