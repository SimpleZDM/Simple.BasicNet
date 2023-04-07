using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Net
 * 接口名称 ConnectionContext
 * 开发人员：11920
 * 创建时间：2023/4/7 10:11:00
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Handle
{
	public class HandleContext
	{
		public HandleContext() { }

		/// <summary>
		/// 缓冲去
		/// </summary>
		public byte[] ReceiveBuffer { get { return receiveBuffer;} set { receiveBuffer = value; } }

		/// <summary>
		/// 消息头
		/// </summary>
		public byte[] HeaderBuffer { get { return headerBuffer; } set { headerBuffer = value; } }
		/// <summary>
		/// 消息尾部
		/// </summary>
		public byte[] TailBuffer { get { return tailBuffer; } set { tailBuffer = value; } }

		/// <summary>
		/// 消息id
		/// </summary>
		public int CommandID { get { return commandId; } set { commandId = value; } }
		/// <summary>
		/// 消息长度
		/// </summary>
		public int Length { get { return length; } set { length = value; } }
	
		private byte[] receiveBuffer;
		private int commandId;
		private byte[] headerBuffer;
		private byte[] tailBuffer;
		private int length;
		private Action<byte[]> send;
		public void Send(byte[] bytes)
		{
			send.Invoke(bytes);
		}

		public void Send(string msg)
		{
			Send(Encoding.UTF8.GetBytes(msg));
		}

		public void SetSend(Action<byte[]> action)
		{
			send=action;
		}

	}
}
