using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Schedule
 * 接口名称 ScheduleHandle
 * 开发人员：11920
 * 创建时间：2023/4/10 15:03:55
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Schedule
{
	internal class ScheduleHandle:IScheduleHandle
	{
		private ISchedule schedule;

		public ScheduleHandle()
		{
		}

		public void SetSchedule(ISchedule schedule)
		{
			this.schedule = schedule;
		}

		public void Start()
		{
			schedule.ScheduleInfo.ScheduleStatus = eScheduleStatus.Running;
			Excute();
		}

		public void Excute()
		{
			Task.Run(() =>
			{
				while (schedule.ScheduleInfo.ScheduleStatus== eScheduleStatus.Running)
				{
					try
					{
					    schedule.Run();
						Task.Delay(schedule.ScheduleInfo.Period);
					}
					catch (Exception ex)
					{
						schedule.ScheduleInfo.ErrorMessage = ex.Message;
						schedule.ScheduleInfo.ScheduleStatus = eScheduleStatus.Exception;
						break;
					}
					
				}
			});
		}

		public void Stop()
		{
			schedule.ScheduleInfo.ScheduleStatus= eScheduleStatus.Stop;
		}

		public ScheduleInfo GetScheduleInfo()
		{
			return schedule.ScheduleInfo;
		}
	}
}
