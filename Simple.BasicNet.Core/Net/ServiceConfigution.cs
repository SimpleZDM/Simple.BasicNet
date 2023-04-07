using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Net
 * 接口名称 ServiceConfigution
 * 开发人员：11920
 * 创建时间：2023/4/6 15:39:39
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Net
{
	public class ServiceConfigution
	{
		public ServiceConfigution() {

			ipAddress = IPAddress.Any;
			Backlog= 310;
		}

		public void SetIpAddress(string IpString)
		{
			if (!IPAddress.TryParse(IpString, out ipAddress))
				throw new Exception("ip地址不合法请检查!");
			
		}
		public IPAddress GetIpAddress()
		{
			return ipAddress;
		}
		public int Port { get { return port; } set { if (port <= 1000) throw new Exception(); port = value; } }
		public SocketType SocketType { get { return socketType; } set { socketType = value; } }
		public ProtocolType ProtocolType { get { return protocolType; } set { protocolType = value; } }
		//
		// 摘要:
		//     Places a System.Net.Sockets.Socket in a listening state.
		//
		// 参数:
		//   backlog:
		//     The maximum length of the pending connections queue.
		//
		// 异常:
		//   T:System.Net.Sockets.SocketException:
		//     An error occurred when attempting to access the socket.
		//
		//   T:System.ObjectDisposedException:
		//     The System.Net.Sockets.Socket has been closed.
		public int Backlog { get { return backlog; } set { backlog = value; } }

		private IPAddress ipAddress;
		private int port;
		private SocketType socketType;
		private ProtocolType protocolType;
		private int backlog;

		
	}
}
