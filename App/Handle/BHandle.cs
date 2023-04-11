using Simple.BasicNet.Core.Atrributes;
using Simple.BasicNet.Core.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 App.Handle
 * 接口名称 BHandle
 * 开发人员：11920
 * 创建时间：2023/4/6 10:17:13
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace App.Handle
{
    [NumberRange(21, 30)]
	public class BHandle: BaseHandle
	{

		public BHandle() { }

		[MessageNumber(22)]
		public void Test22()
		{
			string msg = "我是:BHandle;我的消息号码为22！";
			Console.WriteLine(msg);
		    context.SendUTF8(msg);
		}

		public void Test23()
		{
			string msg = "我是:BHandle;我的消息号码为23！";
			Console.WriteLine(msg);
			context.SendUTF8(msg);
		}
	}
}
