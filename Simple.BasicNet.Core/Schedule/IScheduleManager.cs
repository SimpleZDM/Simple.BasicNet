using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Schedule
 * 接口名称 IScheduleManager
 * 开发人员：11920
 * 创建时间：2023/4/10 15:49:23
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Schedule
{
	public interface IScheduleManager
	{
		/// <summary>
		/// 注册任务
		/// </summary>
		/// <typeparam name="TSchedule"></typeparam>
		public void Register<TSchedule>() where TSchedule : ISchedule;
		/// <summary>
		/// 开始所有任务
		/// </summary>
		public void Start();
		public void Start(Guid Id);
		public void Stop(Guid Id);
		public void Stop();
		public IEnumerable<ScheduleInfo> GetSchedule();
	}
}
