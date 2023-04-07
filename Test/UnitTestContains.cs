using Simple.BasicNet.Core;
using System.ComponentModel;
using MyContainer= Simple.BasicNet.Core.Container;
using IMyContainer= Simple.BasicNet.Core.IContainer;
using NUnit.Framework.Internal;
using Test.Entity;

namespace Test
{
	[TestFixture]
	public class UnitTestContains
	{

		public IMyContainer container { get; set; }
		[SetUp]
		public void Setup()
		{
			container = MyContainer.BuilderContainer();
			container.Register<ITestA, TestA>();
			container.Register<ITestB, TestB>();
		}

		[Test]
		public void TestResgister()
		{
			TestDelegate testDelegate =()=> {
				container.Register<ITestA, TestA>();
				container.Register<ITestB, TestB>();
			};
			Assert.DoesNotThrow(testDelegate, "container.Resgister;存在异常!"); 
		}

		[Test]
		public void TestGetService()
		{
			ITestA testA=container.GetService<ITestA>();
			ITestA testA1=container.GetService<TestA>();
			ITestB testb=container.GetService<ITestB>();
			Assert.IsNotNull(testA, "container.GetService;获取实例testA异常!");
			Assert.IsNotNull(testA1, "container.GetService;获取实例TestA1异常!");
			Assert.IsNotNull(testb, "container.GetService;获取实例Testb异常!");
		}
	}
}