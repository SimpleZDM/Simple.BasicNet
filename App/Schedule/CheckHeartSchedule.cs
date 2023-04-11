using Simple.BasicNet.Core.Atrributes;
using Simple.BasicNet.Core.Net;
using Simple.BasicNet.Core.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 App.Schedule
 * 接口名称 CheckSchedule
 * 开发人员：11920
 * 创建时间：2023/4/11 11:31:31
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace App.Schedule
{
	internal class CheckHeartSchedule : ISchedule
	{
		public CheckHeartSchedule(IClientManager clientManager)
		{
			ScheduleInfo=new ScheduleInfo();
			ScheduleInfo.Period = 10000;
			this.clientManager= clientManager;
			messageBuffer = Encoding.UTF8.GetBytes("检查!");
		}
		private IClientManager clientManager;
		public ScheduleInfo ScheduleInfo { get { return scheduleInfo; }
			set {scheduleInfo=value; } }
		private byte[] messageBuffer ;
		public void Run()
		{

			Console.WriteLine($"########心跳包:{DateTime.Now}########");
			Console.WriteLine($"########次数:{count}########");
			Console.WriteLine($"########客户端数量:{clientManager.GetClientCount()}########");
			foreach (var item in clientManager.GetClients())
			{
				Console.WriteLine($"########ID:{item.ConnectionID}########");
				item.Send(messageBuffer);
			}
			count++;
		}
		private ScheduleInfo scheduleInfo;
		private int count;
	}
}
