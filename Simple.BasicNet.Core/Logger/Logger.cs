using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Logger
 * 接口名称 Logger
 * 开发人员：11920
 * 创建时间：2023/4/11 13:58:10
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core
{
	internal class Logger : ILogger
	{
		public void Error(string context)
		{
			ConsoleWrite($"Error:{context}", ConsoleColor.Red);
		}

		public void Info(string context)
		{
			ConsoleWrite($"Info:{context}");
		}

		public void Warn(string context)
		{
			ConsoleWrite($"Warn:{context}",ConsoleColor.Yellow);
		}

		private void ConsoleWrite(string context,ConsoleColor color=ConsoleColor.White)
		{
#if DEBUG
				ConsoleColor currentForeColor = Console.ForegroundColor;
				Console.ForegroundColor = color;
			    Console.WriteLine($"##########DATE:{DateTime.Now};{context}##########");
				Console.ForegroundColor = currentForeColor;
#endif
		}
	}
}
