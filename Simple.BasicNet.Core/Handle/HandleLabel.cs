using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Net
 * 接口名称 HandleLabel
 * 开发人员：11920
 * 创建时间：2023/4/7 11:46:31
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Handle
{
	internal class HandleLabel
	{

		public HandleLabel() 
		{ 

		}
		public bool IsRange(int target)
		{
			if(MaxNumber>=target&&MinNumber<=target)
				return true;
			return false;
		}
		public int MaxNumber { get { return max; } set { max = value; } }
		public int MinNumber { get { return min; } set { min = value; } }
		public Type HandType { get { return handType; } set { handType = value; } }

		private int max;
		private int min;
		private Type handType;
	}
}
