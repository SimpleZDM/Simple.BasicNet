using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TTarget"></typeparam>
		public ITypeMapperExternal Register<TTarget>();
		
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TOriginal"></typeparam>
		/// <typeparam name="TTarget"></typeparam>
		public ITypeMapperExternal Register<TOriginal, TTarget>();
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="targetType"></param>
		public ITypeMapperExternal Register(Type targetType);
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="targetTypes"></param>
		public ITypeMapperExternal Register(Type[] targetTypes);
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="originalType"></param>
		/// <param name="targetType"></param>
		public ITypeMapperExternal Register(Type originalType, Type targetType);
	


		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TTarget"></typeparam>
		public ITypeMapperExternal RegisterSingleton<TTarget>();
		
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TOriginal"></typeparam>
		/// <typeparam name="TTarget"></typeparam>
		public ITypeMapperExternal RegisterSingleton<TOriginal, TTarget>();
	

		public ITypeMapperExternal RegisterSingleton<TTarget>(TTarget target) where TTarget : class;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Target"></param>
		public ITypeMapperExternal RegisterSingleton(Type Target)
		{
			return RegisterSingleton(Target, Target);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Original"></param>
		/// <param name="Target"></param>
		public ITypeMapperExternal RegisterSingleton(Type Original, Type Target);
	

		/// <summary>
		/// 
		/// </summary>
		/// <param name="original"></param>
		/// <param name="target"></param>
		public ITypeMapperExternal RegisterAssambly(Assembly original, Assembly target);
	
		public Target GetService<Target>();
		public Target GetService<Target>(string token);
		public object GetService(Type target, string token);
		public object GetService(Type target);

		
	}
}
