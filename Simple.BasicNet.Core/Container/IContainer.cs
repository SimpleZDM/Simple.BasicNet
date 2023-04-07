using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Container
 * 接口名称 IContainer
 * 开发人员：11920
 * 创建时间：2023/4/6 11:28:33
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core
{
	public interface IContainer
	{
		public void Register<TTarget>();
		public void Register<TOriginal, TTarget>();
		public void Register(Type targetType);
		public void Register(Type originalType, Type targetType);
		public void RegisterSingleton<TTarget>();
		public void RegisterSingleton<TOriginal, TTarget>();

		public void RegisterSingleton<TTarget>(TTarget target) where TTarget : class;
		public void RegisterSingleton(Type Target);
		public void RegisterSingleton(Type Original, Type Target);
		public Target GetService<Target>();
		public object GetService(Type target);

		
	}
}
