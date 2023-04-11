using Simple.BasicNet.Core.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 App.Schedule
 * 接口名称 TestSchedule
 * 开发人员：11920
 * 创建时间：2023/4/11 10:49:55
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace App.Schedule
{
	internal class TestSchedule : ISchedule
	{
		public TestSchedule() {

			ScheduleInfo=new ScheduleInfo();
			ScheduleInfo.Period = 10000;
			ScheduleInfo.Title = "测试任务";
			ScheduleInfo.Description = "测试任务";
		}
		public ScheduleInfo ScheduleInfo 
		{ 
			get { return scheduleInfo; }
			set { scheduleInfo = value; }
		}

		public void Run()
		{
			Console.WriteLine($"当前时间:{DateTime.Now}#############Test");
			if (count==10)
			{
				throw new Exception("");
			}
			count++;
		}
		private ScheduleInfo scheduleInfo;
		private int count;
	}
}
