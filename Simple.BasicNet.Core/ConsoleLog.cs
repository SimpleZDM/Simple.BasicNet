using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core
 * 接口名称 ConsoleLog
 * 开发人员：11920
 * 创建时间：2023/4/7 10:37:58
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core
{
	internal static class ConsoleLog
	{
		public static void DEBUGLOG(string msg)
		{
#if DEBUG
			Console.WriteLine($"######{DateTime.Now}###{msg}");
#endif
		}
	}
}
