using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Test.Entity
 * 接口名称 TestB
 * 开发人员：11920
 * 创建时间：2023/4/6 14:32:41
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Test.Entity
{
	public class TestB:ITestB
	{

		public TestB(ITestA testA) 
		{
			
		}
	}

	public interface ITestB
	{

	}
}
