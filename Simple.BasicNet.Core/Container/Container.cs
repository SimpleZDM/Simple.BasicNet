using Simple.BasicNet.Core.Atrributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core
 * 接口名称 Container
 * 开发人员：11920
 * 创建时间：2023/4/6 11:27:04
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core
{
	public class Container:IContainer
	{

		public static IContainer BuilderContainer()
		{
			return new Container();
		}
		//接口名称对应,类名称
		private ConcurrentDictionary<string, TypeMapper> typeMapperDic;

		public Container()
		{
			typeMapperDic = new ConcurrentDictionary<string, TypeMapper>();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TTarget"></typeparam>
		public void Register<TTarget>()
		{
			Register(typeof(TTarget));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TOriginal"></typeparam>
		/// <typeparam name="TTarget"></typeparam>
		public void Register<TOriginal,TTarget>()
		{
			Register(typeof(TOriginal), typeof(TTarget));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="targetType"></param>
		public void Register(Type targetType)
		{
			TypeMapper typeMapper = new TypeMapper(targetType);
			Register(typeMapper);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="targetTypes"></param>
		public void Register(Type [] targetTypes)
		{
			foreach (var item in targetTypes)
			{
				Register(item);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="originalType"></param>
		/// <param name="targetType"></param>
		public void Register(Type originalType, Type targetType)
		{
			TypeMapper typeMapper = new TypeMapper(originalType,targetType);
			Register(typeMapper);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TTarget"></typeparam>
		public void RegisterSingleton<TTarget>()
		{
			RegisterSingleton(typeof(TTarget), typeof(TTarget));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TOriginal"></typeparam>
		/// <typeparam name="TTarget"></typeparam>
		public void RegisterSingleton<TOriginal, TTarget>()
		{
			RegisterSingleton(typeof(TOriginal), typeof(TTarget));
		}


		public void RegisterSingleton<TTarget>(TTarget target)where TTarget : class
		{
			TypeMapper typeMapper = new TypeMapper(target.GetType(), eLifeCycle.Single);
			Register(typeMapper);
			typeMapper.Instance =target;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Target"></param>
		public void RegisterSingleton(Type Target)
		{
			RegisterSingleton(Target, Target);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Original"></param>
		/// <param name="Target"></param>
		public void RegisterSingleton(Type Original,Type Target)
		{
			TypeMapper typeMapper = new TypeMapper(Original,Target,eLifeCycle.Single);
			Register(typeMapper);
			typeMapper.Instance = GetService(Target);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="original"></param>
		/// <param name="target"></param>
		public void RegisterAssambly(Assembly original,Assembly target)
		{
			foreach (var OriginalType in original.GetTypes().Where(t=>t.IsInterface))
			{
				if (target.GetTypes().Any(t=>!t.IsValueType&&t.IsAssignableTo(OriginalType)))
				{
					var TargetType = target.GetTypes().FirstOrDefault(t=>t.IsAssignableTo(OriginalType));
					Register(OriginalType,TargetType);
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="Target"></typeparam>
		/// <returns></returns>
		public Target GetService<Target>()
		{
			Type targetType= typeof(Target);
			return (Target)GetService(targetType);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public object GetService(Type target)
		{
			if (target==typeof(IContainer)||target==typeof(Container))
			{
				return this;
			}
			return Create(target);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="typeMapper"></param>
		private void Register(TypeMapper typeMapper)
		{
			typeMapper.Verify();
			string key = typeMapper.GetTypeMapperKey();
			if (!typeMapperDic.ContainsKey(key))
			{
				typeMapperDic.TryAdd(key, typeMapper);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		private object Create(Type target)
		{
			object o = null;
			if (typeMapperDic.Where(mapper => mapper.Value.IsTargetType(target)).Any())
			{
				var typeMapper = typeMapperDic.FirstOrDefault(mapper => mapper.Value.IsTargetType(target)).Value;
				//单例就返回
				if (typeMapper.LifeCycle == eLifeCycle.Single&&typeMapper.Instance!=null)
				{
					return typeMapper.Instance;
				}
				//初始化构造方法
				o = Activator.CreateInstance(typeMapper.Target,CreateParameters(typeMapper.Target.GetConstructors().FirstOrDefault()));
				//配置属性

				CreateProperties(typeMapper.Target,o);

			}
			return o;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="constructor"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		private object [] CreateParameters(ConstructorInfo constructor)
		{
			if (constructor == null)
				throw new Exception("获取构造方法失败,请检查是否将接口注入到容器中,接口不具有构造方式!");
			object[] Params = new object[constructor.GetParameters().Count()];
			int i = 0;
			foreach (var Parameter in constructor.GetParameters())
			{
				Params[i] = GetService(Parameter.ParameterType);
				i++;
			}
			return Params;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Type"></param>
		/// <param name="o"></param>
		private void CreateProperties(Type Type,object o)
		{
			foreach (var Property in Type.GetProperties().Where(p=>p.IsDefined(typeof(AutowiredAttribute))))
			{
				Property.SetValue(o,GetService(Property.PropertyType));
			}
		}
		
	}
}
