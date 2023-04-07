using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Atrributes
 * 接口名称 NumberRangeAttribute
 * 开发人员：11920
 * 创建时间：2023/4/6 10:45:43
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Atrributes
{
	[AttributeUsage(AttributeTargets.Class)]	
	public class NumberRangeAttribute:Attribute
	{
		private  int maxMessageNumber;
		private int minMessageNumber;
		public NumberRangeAttribute(int min,int max)
		{
			if (min<=max)
			{
				MaxMessageNumber = max;
				MinMessageNumber = min;
			}
			else
			{
				MaxMessageNumber = min;
				MinMessageNumber = max;
			}
			
		}

		public int MaxMessageNumber { get { return maxMessageNumber; } set { maxMessageNumber = value; } }
		public int MinMessageNumber { get { return minMessageNumber; } set { minMessageNumber = value; } }

	}
}
