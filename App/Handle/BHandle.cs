﻿using Simple.BasicNet.Core;
using Simple.BasicNet.Core.Atrributes;
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
			Console.WriteLine("我是:BHandle;我的消息号码为22！");
		}

		public void Test23()
		{
			Console.WriteLine("我是:BHandle;我的消息号码为23！");
		}
	}
}