using Simple.BasicNet.Core.Atrributes;
using Simple.BasicNet.Core.Handle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 App.Handle
 * 接口名称 CHandle
 * 开发人员：11920
 * 创建时间：2023/4/7 16:54:39
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace App.Handle
{
	[NumberRange(41, 50)]
	internal class CHandle:BaseHandle
	{
		public CHandle() { }

		[MessageNumber(44)]
		public void Test44()
		{
			string msg = "我是:BHandle;我的消息号码为44！";
			Console.WriteLine(msg);
			context.Send(msg);
		}

		public void Test23()
		{
			string msg = "我是:BHandle;我的消息号码为23！";
			Console.WriteLine(msg);
			context.Send(msg);
		}
	}
}
