using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Schedule
 * 接口名称 IScheduleHandle
 * 开发人员：11920
 * 创建时间：2023/4/10 15:12:09
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Schedule
{
	internal interface IScheduleHandle
	{
		public void Start();
		public void Stop();
		public ScheduleInfo GetScheduleInfo();
	}
}
