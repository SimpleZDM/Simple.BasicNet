using Simple.BasicNet.Core.Atrributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Entity;

/*********************************************************
 * 命名空间 Test.Entity
 * 接口名称 TestC
 * 开发人员：11920
 * 创建时间：2023/4/7 15:38:31
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Test
{
	public class TestC: ITestC
	{
		[Autowired]
		public ITestB testB { get; set; }


		[Autowired]
		[InstanceKey(typeof(TestD1))]
		public ITestD testd1 { get; set; }
		[TestAutowired]
		[InstanceKey(typeof(TestD))]
		public ITestD testd { get; set; }
		public ITestD ctorTest { get; set; }
		public TestC([InstanceKey(typeof(TestD))] ITestD testD)
		{
			ctorTest = testD;
		} 
	}

	public interface ITestC
	{

	}
}
