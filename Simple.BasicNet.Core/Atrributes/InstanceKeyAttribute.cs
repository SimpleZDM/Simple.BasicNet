using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Atrributes
 * 接口名称 InstanceKeyAttribute
 * 开发人员：11920
 * 创建时间：2023/4/10 11:01:09
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Atrributes
{
	[AttributeUsage(AttributeTargets.Property|AttributeTargets.Parameter)]
	public class InstanceKeyAttribute:Attribute
	{
		public string token;
		public InstanceKeyAttribute(Type type):this(type.FullName) 
		{
		}

		public InstanceKeyAttribute(string token)
		{
			this.token = token;
		}

		public string GetKey()
		{
			return this.token;
		}
	}
}
