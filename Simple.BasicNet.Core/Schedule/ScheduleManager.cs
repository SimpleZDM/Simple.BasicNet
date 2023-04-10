using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Schedule
 * 接口名称 Schedule
 * 开发人员：11920
 * 创建时间：2023/4/10 14:57:07
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Schedule
{
	internal class ScheduleManager:IScheduleManager
	{

		ConcurrentDictionary<Guid,IScheduleHandle> dicSchedule;
		IContainer container;
		public ScheduleManager(IContainer container)
		{
			dicSchedule=new ConcurrentDictionary<Guid, IScheduleHandle>();
			this.container = container;
		}
		/// <summary>
		/// 注册任务
		/// </summary>
		/// <typeparam name="TSchedule"></typeparam>
		public void Register<TSchedule>() where TSchedule:ISchedule
		{
			container.RegisterSingleton<ISchedule,TSchedule>().Autowird();
			var schedule=container.GetService<ISchedule>(typeof(TSchedule).FullName);
			var scheduleHandle = container.GetService<ScheduleHandle>();
			scheduleHandle.SetSchedule(schedule);
			dicSchedule.TryAdd(schedule.ScheduleInfo.Id,scheduleHandle);
		}
		/// <summary>
		/// 开始所有任务
		/// </summary>
		public void Start()
		{
			foreach (var item in dicSchedule.Values)
			{
				item.Start();
			}
		}
		public void Start(Guid Id)
		{
			if (dicSchedule.ContainsKey(Id))
			{
				dicSchedule[Id].Start();
			}
		}
		public void Stop(Guid Id)
		{
			if (dicSchedule.ContainsKey(Id))
			{
				dicSchedule[Id].Stop();
			}
		}
		public void Stop()
		{
			foreach (var item in dicSchedule.Values)
			{
				item.Stop();
			}
		}
		public IEnumerable<ScheduleInfo>GetSchedule()
		{
			return dicSchedule.Select(s=>s.Value.GetScheduleInfo());
		}

	}
}
