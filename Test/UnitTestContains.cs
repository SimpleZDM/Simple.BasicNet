using Simple.BasicNet.Core;
using System.ComponentModel;
//using MyContainer= Simple.BasicNet.Core.Container;
using IMyContainer= Simple.BasicNet.Core.IContainer;
using NUnit.Framework.Internal;
using Test.Entity;
using NUnit.Framework.Interfaces;

namespace Test
{
	[TestFixture]
	public class UnitTestContains
	{

		public IMyContainer container { get; set; }
		[SetUp]
		public void Setup()
		{
			//container =MyContainer.GetContainer();
			container.Register<ITestA, TestA>();
			container.Register<ITestB, TestB>();
			container.Register<ITestC, TestC>().Autowird(typeof(TestAutowiredAttribute));
			container.Register<ITestD, TestD>();
			container.Register<ITestD, TestD1>();
			//container.RegisterSingleton<ITestD, TestD>();
		}

		[Test]
		public void TestResgister()
		{
			TestDelegate testDelegate =()=> {
				container.Register<ITestA, TestA>();
				container.Register<ITestB, TestB>();
			};
			Assert.DoesNotThrow(testDelegate, "ע���������;�����쳣!"); 
		}

		[Test]
		public void TestGetService()
		{
			ITestA testA=container.GetService<ITestA>();
			ITestA testA1=container.GetService<TestA>();
			ITestB testb=container.GetService<ITestB>();
			Assert.IsNotNull(testA, "��ͨ��ȡʵ��;��ȡʵ��testA�쳣!");
			Assert.IsNotNull(testA1, "��ͨ��ȡʵ��;��ȡʵ��TestA1�쳣!");
			Assert.IsNotNull(testb, "��ͨ��ȡʵ��;��ȡʵ��Testb�쳣!");
		}

		[Test]
		public void TestGetServiceAutowired()
		{
			ITestC testC = container.GetService<ITestC>();
			Assert.IsNotNull(testC, "��������ע���������!");
		}

		[Test]
		public void TestGetServiceSinial()
		{
			ITestD testD = container.GetService<TestD>();
			ITestD testD1 = container.GetService<TestD>();
			bool result = object.ReferenceEquals(testD, testD1);
			Assert.IsTrue(result, "���Ե��������ò�һ��������ʧ��!");
		}
	}
}