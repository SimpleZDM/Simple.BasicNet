using Simple.BasicNet.Core.Atrributes;
using Simple.BasicNet.Core.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Net
 * 接口名称 CoreNet
 * 开发人员：11920
 * 创建时间：2023/4/7 17:43:30
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Net
{
	public class Pretty:IPretty
	{
		private	Socket pretty;
		[Autowired]
		private ServiceConfigution serviceConfigution { get; set; }	
		[Autowired]
		private IClientManager clientManager { get;set; }
		public Pretty()
		{
		}
		public void Start()
		{
			Initalization();
			Accept();
		}

		public void Initalization()
		{
			IPEndPoint endPoint = new IPEndPoint(serviceConfigution.GetIpAddress(), serviceConfigution.Port);
			pretty = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			pretty.Bind(endPoint);
			pretty.Listen(serviceConfigution.Backlog);
		}

		public void Accept()
		{
			Task.Run(() =>
			{
				while (true)
				{
					try
					{
						var client = pretty.Accept();
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
			});
		}
	}
}
