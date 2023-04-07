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
		public void CheckHeart();
		
	}
}
