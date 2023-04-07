using Simple.BasicNet.Core.Atrributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 App.Handle
 * 接口名称 TestHandle
 * 开发人员：11920
 * 创建时间：2023/4/6 10:50:14
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace App.Handle
{
	[NumberRange(31, 40)]
	public class TestHandle:AHandle
	{
		public TestHandle() { }

		[MessageNumber(33)]
		public void Test33()
		{
			string msg = "我是:TestHandle;我的消息号码为33！";
			Console.WriteLine(msg);
			context.Send(msg);
		}
		[MessageNumber(32)]
		public void Test32()
		{
			string msg = "我是:TestHandle;我的消息号码为32！";
			Console.WriteLine(msg);
			context.Send(msg);
		}
	}
}
