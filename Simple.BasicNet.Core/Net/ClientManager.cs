using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Net
 * 接口名称 SocketManager
 * 开发人员：11920
 * 创建时间：2023/4/6 15:35:10
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Net
{
	public class ClientManager
	{
		ConcurrentDictionary<Socket,ClientSocket> clientsDic;
		private Type messageHandleType;
		private IContainer container;
		public ClientManager(IContainer container)
		{
			clientsDic=new ConcurrentDictionary<Socket, ClientSocket>();
			this.container = container;
		}

		public void AddClient(Socket client)
		{
			if (!clientsDic.ContainsKey(client))
			{
				ClientSocket clientSocket = new ClientSocket(container,client);
				clientsDic.TryAdd(client,clientSocket);
			}
		}
	}
}
