using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Schedule
 * 接口名称 ScheduleInfo
 * 开发人员：11920
 * 创建时间：2023/4/10 15:17:52
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Schedule
{
	public class ScheduleInfo
	{
		public ScheduleInfo() 
		{
			Id=Guid.NewGuid();
		}
		public eScheduleStatus ScheduleStatus { get { return scheduleStatus; } set { scheduleStatus = value; } }
		public string ErrorMessage { get { return errorMessage; } set { errorMessage = value; } }
		public string Title { get { return title; } set { title = value; } }
		public string Description { get { return description; } set { description = value; } }
		public int  Period  { get { return period; } set { period = value; } }
		public Guid  Id  { get { return id; } private set {  id = value; } }

		private eScheduleStatus scheduleStatus;
		private string errorMessage;
		private string title;
		private string description;
		private int period;
		private Guid id;
	}
}
