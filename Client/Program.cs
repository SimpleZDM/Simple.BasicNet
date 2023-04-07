


using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;

public class Progarm
{
	public static void Main(string[]args)
	{
		
		 data1 =new byte[]{0xB,0xC, 0xB, 0xF,0x11};//11 
		 data2 =new byte[]{0xB,0xC, 0x16, 0xF,0x11};//22=16+6
		 data3 =new byte[]{0x21,0xC, 0x21, 0xF,0x11};//33 16*2+1
		 data4 =new byte[]{0x2D,0xC, 0x2C, 0xF,0x11};//44 16*2+12
		 StarTest();
	}

	public static void StarTest()
	{
		string strIp = "172.16.1.128";
		IPAddress.TryParse("172.16.1.128",out IPAddress ip);
		int Port = 1234;
		IPEndPoint endPoint = new IPEndPoint(ip, 1234);
		Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
		socket.Connect(endPoint);
		int length =0;
		byte[] bufferContain = new byte[1024];
		byte[] buffer = new byte[1024];
		Console.WriteLine("#################链接成功################");
		Console.WriteLine($"#################服务器地址:{strIp}################");
		Console.WriteLine($"#################端口:{Port}################");
		Task.Run(() =>
		{
			while (true)
			{
				length = socket.Receive(bufferContain);
				buffer=new byte[length];
				Array.Copy(bufferContain,buffer,length);
				Console.WriteLine(Encoding.UTF8.GetString(buffer));
			}
		});

		while (true)
		{
			string cmd=Console.ReadLine();
			int.TryParse(cmd,out int cmdId);
				if(cmdId <= 0)
			break;

			switch (cmdId)
			{
				case 1:
					socket.Send(data1);
					break;
				case 2:
					socket.Send(data2);
					break;
				case 3:
					socket.Send(data3);
					break;
				case 4:
					socket.Send(data4);
					break;
				default:
					break;
			}
		}
		
	}


	public static byte[] data1;	
	public static byte[] data2;	
	public static byte[] data3;		
	public static byte[] data4;		
}