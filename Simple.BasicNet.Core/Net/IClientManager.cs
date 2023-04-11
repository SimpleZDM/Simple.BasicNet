using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Simple.BasicNet.Core.Net
{
	public interface IClientManager
	{
		public bool AddClient(Socket client);
		public void RemoveClient(Guid ClientID);
		public IEnumerable<ClientSocket> GetClients();
		public ClientSocket GetClient(Guid key);
		public IEnumerable<ClientSocket> GetClient(Guid[] keys);

		public int GetClientCount();
	}
}
