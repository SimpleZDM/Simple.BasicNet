using App.Handle;
using Simple.BasicNet.Core;
using System.Threading;

public class Program
{
	private static ManualResetEvent mre = new ManualResetEvent(false);
	public static int Main(string[] args)
	{
		Type type=typeof(AHandle);

		
		FindBaseHandleChildren find = new FindBaseHandleChildren();
		while (true)
		{
			string strCommand = Console.ReadLine();
			int.TryParse(strCommand,out int number);

			find.Find(number);
			if (number==0)
			{
				break;
			}
		}
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
