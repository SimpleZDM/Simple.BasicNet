using App.Handle;
using App.Schedule;
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


		IHost host= Host.BuilderHost().Regster((contain) =>
		{
			//容器模块，注册需要实例的对象，容器可以自动创建对象，解决对象的依赖。

		}).
		RegisterSchedule((scheduleManager) =>
		{
			//注册任务
			//任务是固定时间间隔执行
			//一个任务周期为 任务中逻辑时间+等待时间
			//通常任务时间比较少，适合做心跳检查
			scheduleManager.Register<TestSchedule>();
			scheduleManager.Register<CheckHeartSchedule>();

		}).Start((configution) =>
		{
			configution.Port = 1234;
			configution.Backlog = 200;
			configution.ProtocolType=System.Net.Sockets.ProtocolType.Tcp;
			configution.SetIpAddress("172.16.1.128");
			configution.SocketType = System.Net.Sockets.SocketType.Stream;
		});
		Console.ReadLine();
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
