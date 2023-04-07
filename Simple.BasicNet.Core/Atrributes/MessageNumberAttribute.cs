using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core
 * 接口名称 Attribute
 * 开发人员：11920
 * 创建时间：2023/4/6 10:38:17
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Atrributes
{

	/// <summary>
	/// 消息编
	/// </summary>
	/// 
	[AttributeUsage(AttributeTargets.Method)]
	public class MessageNumberAttribute:Attribute
	{
		private int number;
		public MessageNumberAttribute(int number) 
		{
			this.number=number;
		}

		public int Number { get { return number; } set{ number = value; } }
	}
}
