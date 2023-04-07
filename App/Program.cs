using App.Handle;
using Newtonsoft.Json;
using Simple.BasicNet.Core;
using Simple.BasicNet.Core.Handle;
using Simple.BasicNet.Core.Net;
using System.Threading;

public class Program
{
	private static ManualResetEvent mre = new ManualResetEvent(false);
	public static int Main(string[] args)
	{
		var str =File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration.json"));

		var Cinfiguration =JsonConvert.DeserializeObject<ServiceConfigution>(str);
		Host host= new Host();
		host.Start((configution) =>
		{
			configution.Port = 1234;
			configution.Backlog = 200;
			configution.ProtocolType=System.Net.Sockets.ProtocolType.Tcp;
			configution.SetIpAddress("172.16.1.128");
			configution.SocketType = System.Net.Sockets.SocketType.Stream;
		});
		//Type type=typeof(AHandle);

		
		//FindBaseHandleChildren find = new FindBaseHandleChildren();
		//while (true)
		//{
		//	string strCommand = Console.ReadLine();
		//	int.TryParse(strCommand,out int number);

		//	find.Find(number);
		//	if (number==0)
		//	{
		//		break;
		//	}
		//}
		return 0;
	}
	private static void ThreadProc()
	{
		string name = Thread.CurrentThread.Name;

		Console.WriteLine(name + " starts and calls mre.WaitOne()");

		mre.WaitOne();

		Console.WriteLine(name + " ends.");
	}
}
