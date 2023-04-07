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
			Assert.DoesNotThrow(testDelegate, "container.Resgister;�����쳣!"); 
		}

		[Test]
		public void TestGetService()
		{
			ITestA testA=container.GetService<ITestA>();
			ITestA testA1=container.GetService<TestA>();
			ITestB testb=container.GetService<ITestB>();
			Assert.IsNotNull(testA, "container.GetService;��ȡʵ��testA�쳣!");
			Assert.IsNotNull(testA1, "container.GetService;��ȡʵ��TestA1�쳣!");
			Assert.IsNotNull(testb, "container.GetService;��ȡʵ��Testb�쳣!");
		}
	}
}