using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Schedule
 * 接口名称 EnumType
 * 开发人员：11920
 * 创建时间：2023/4/10 14:59:53
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Schedule
{
	public enum eScheduleStatus
	{
		Stop=0,
		Running,
		Waitting,
		Exception,
	}
}
