using Simple.BasicNet.Core.Atrributes;
using Simple.BasicNet.Core.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 App.Handle
 * 接口名称 AHandle
 * 开发人员：11920
 * 创建时间：2023/4/6 10:16:53
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
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
			Console.WriteLine("我是:Ahandle;我的消息号码为11！");
		}


		[MessageNumber(12)]
		public void Test12()
		{
			Console.WriteLine("我是:Ahandle;我的消息号码为12！");
		}
	}
}
