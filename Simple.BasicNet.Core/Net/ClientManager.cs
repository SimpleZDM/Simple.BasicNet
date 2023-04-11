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
	public class ClientManager: IClientManager
	{
	    private	ConcurrentDictionary<Guid,ClientSocket> clientsDic;
		private IContainer container;
		public ClientManager(IContainer container)
		{
			clientsDic=new ConcurrentDictionary<Guid, ClientSocket>();
			this.container = container;
		}

		public bool AddClient(Socket client)
		{
				ClientSocket clientSocket = new ClientSocket(container,client);
				return clientsDic.TryAdd(clientSocket.ConnectionID, clientSocket);
		}

		

		public  void RemoveClient(Guid ClientID)
		{
			if (clientsDic.ContainsKey(ClientID))
			{
				clientsDic.TryRemove(ClientID,out ClientSocket client);
			}
		}

		public IEnumerable<ClientSocket> GetClients()
		{
			return clientsDic.Values;
		}

		public ClientSocket GetClient(Guid key)
		{
			if (clientsDic.ContainsKey(key))
			{
				return clientsDic[key];
			}
			return null;
		}
		public IEnumerable<ClientSocket> GetClient(Guid [] keys)
		{
			List<ClientSocket> clients = new List<ClientSocket>();
			foreach (var key in keys)
			{
				var client=GetClient(key);
				if (client!=null)
				{
					clients.Add(client);
				}
			}
			return clients;
		}

		public int GetClientCount()
		{
			return clientsDic.Count();
		}

	}
}
