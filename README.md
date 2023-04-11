# Simple.BasicNet
        封装了.Net6 socket 使用起来非常方便,也很方便扩展. 提供的例子,客户端发送消息编号,对应的服务端解析出编号调用相关方法. 
    这种模式有点类似与asp.net croe中根据路由调用相关处理方法.
    
##1.内置一个小容器
    可以帮助你构造对象,以及解决构造对象过程中的依赖问题.
    
##2.定时任务
    使用者可以方便轻松的使用.做一下扩展开发
    
##3.配置简单
Demo
```csharp 
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

        
```

##4.相应处理对象
```csharp 
namespace App.Handle
{
    [NumberRange(10,20)]
	public class AHandle:BaseHandle
	{
		public AHandle()
		{

		}

		[MessageNumber(11)]
		public void Test11()
		{
			string msg = "我是:Ahandle;我的消息号码为11！";
			Console.WriteLine("我是:Ahandle;我的消息号码为11！");
			context.SendASCII(msg);
		}


		[MessageNumber(12)]
		public void Test12()
		{
			string msg = "我是:Ahandle;我的消息号码为12！";
			Console.WriteLine("我是:Ahandle;我的消息号码为12！");
			context.SendUTF8(msg);
		}
	}
}
```
