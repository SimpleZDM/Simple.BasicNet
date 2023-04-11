using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Logger
 * 接口名称 ILogger
 * 开发人员：11920
 * 创建时间：2023/4/11 13:54:56
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core
{
	public interface ILogger
	{
		public void Info(string context);

		public void Warn(string context);

		public void Error(string context);
		
	}
}
