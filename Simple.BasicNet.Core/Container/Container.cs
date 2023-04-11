using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Simple.BasicNet.Core.Atrributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
	internal class Container:IContainer
	{
		private static object oLock = new object();
		private static IContainer instance;
		public static IContainer SetContainer(IContainer container)
		{
			instance = container;
			return instance;
		}

		public static IContainer GetContainer()
		{
			if (instance==null)
			{
				lock (oLock)
				{
					instance =new Container();
				}
			}
			return instance;
		}



		//接口名称对应,类名称
		private ConcurrentDictionary<string,TypeMapper> typeMapperDic;

		public Container()
		{
			typeMapperDic = new ConcurrentDictionary<string,TypeMapper>();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TTarget"></typeparam>
		public ITypeMapperExternal Register<TTarget>()
		{
			return  Register(typeof(TTarget));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TOriginal"></typeparam>
		/// <typeparam name="TTarget"></typeparam>
		public ITypeMapperExternal Register<TOriginal,TTarget>()
		{
			return Register(typeof(TOriginal),typeof(TTarget));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="targetType"></param>
		public ITypeMapperExternal Register(Type targetType)
		{
			return TypeMapperExternal.Create(RegisterReturnTypeMapper(targetType));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="targetTypes"></param>
		public ITypeMapperExternal Register(Type [] targetTypes)
		{
			List<TypeMapper> typeMappers= new List<TypeMapper>();
			foreach (var item in targetTypes)
			{
				typeMappers.Add(RegisterReturnTypeMapper(item));
			}
			return TypeMapperExternal.Create(typeMappers);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="originalType"></param>
		/// <param name="targetType"></param>
		public ITypeMapperExternal Register(Type originalType, Type targetType)
		{
			TypeMapper typeMapper = GetTypeMapper(originalType,targetType);
			if (typeMapper == null)
			{
				typeMapper = new TypeMapper(originalType, targetType);
			}
			 Register(typeMapper);
			return TypeMapperExternal.Create(RegisterReturnTypeMapper(originalType,targetType));
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TTarget"></typeparam>
		public ITypeMapperExternal RegisterSingleton<TTarget>()
		{
			return RegisterSingleton(typeof(TTarget), typeof(TTarget));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TOriginal"></typeparam>
		/// <typeparam name="TTarget"></typeparam>
		public ITypeMapperExternal RegisterSingleton<TOriginal, TTarget>()
		{
			return RegisterSingleton(typeof(TOriginal), typeof(TTarget));
		}


		public ITypeMapperExternal RegisterSingleton<TTarget>(TTarget target)where TTarget : class
		{
			TypeMapper typeMapper = new TypeMapper(target.GetType(), eLifeCycle.Single);
			Register(typeMapper);
			typeMapper.SetInstance(typeMapper.TargetKey,target);
			return TypeMapperExternal.Create(typeMapper);
		}
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
		public ITypeMapperExternal RegisterSingleton(Type Original,Type Target)
		{
			TypeMapper typeMapper=GetTypeMapper(Original,Target);
			if (typeMapper==null)
			{
				typeMapper = new TypeMapper(Original, Target, eLifeCycle.Single);
			}
			Register(typeMapper);
			//typeMapper.SetInstance(Target.FullName, GetService(Target));
			return TypeMapperExternal.Create(typeMapper);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="original"></param>
		/// <param name="target"></param>
		public ITypeMapperExternal RegisterAssambly(Assembly original,Assembly target)
		{
			List<TypeMapper> typeMappers = new List<TypeMapper>();
			foreach (var OriginalType in original.GetTypes().Where(t=>t.IsInterface))
			{
				if (target.GetTypes().Any(t=>!t.IsValueType&&t.IsAssignableTo(OriginalType)))
				{
					var TargetType = target.GetTypes().FirstOrDefault(t=>t.IsAssignableTo(OriginalType));
					typeMappers.Add(RegisterReturnTypeMapper(OriginalType,TargetType));
				}
			}
			return TypeMapperExternal.Create(typeMappers);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="Target"></typeparam>
		/// <returns></returns>
		public Target GetService<Target>()
		{
			return GetService<Target>(string.Empty);
		}
		public Target GetService<Target>(string token)
		{
			Type targetType = typeof(Target);
			return (Target)GetService(targetType,token);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public object GetService(Type target)
		{
			return GetService(target,string.Empty);
		}
		public object GetService(Type target,string token)
		{
			if (target == typeof(IContainer) || target == typeof(Container))
			{
				return instance;
			}
			return Create(target,token);
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
		private object Create(Type target,string token)
		{
			object o = null;
			TypeMapper typeMapper = FindTargetType(target,token);
			if (typeMapper!=null)
			{
				//初始化构造方法
				if (token==string.Empty)
				{
					 token = typeMapper.TargetKey;

				}
				//单例就返回
				o = typeMapper.GetInstance(token);
				if (typeMapper.LifeCycle == eLifeCycle.Single && o != null)
				{
					return o;
				}
				object[] oParams = CreateParameters(typeMapper.GetTargetType(token).GetConstructors().FirstOrDefault());
				o = Activator.CreateInstance(typeMapper.GetTargetType(token),oParams);
				//配置属性
				if (typeMapper.IsAutowired)
				{
					CreateProperties(typeMapper.GetTargetType(token), o,typeMapper.AutowiredType);
				}
				if (typeMapper.LifeCycle == eLifeCycle.Single)
				{
					typeMapper.SetInstance(token,o);
				}
			}
			return o;
		}

		private TypeMapper FindTargetType(Type target, string token)
		{
			TypeMapper typeMapper = null;
			if (typeMapperDic.ContainsKey(target.FullName))
			{
				typeMapper = typeMapperDic[target.FullName];
			}
			else if (typeMapperDic.Any(Mapper => Mapper.Value.Target.ContainsKey(target.FullName)))
			{
				typeMapper = typeMapperDic.FirstOrDefault(Mapper => Mapper.Value.Target.ContainsKey(target.FullName)).Value;
			}
			return typeMapper;
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
				string token = string.Empty;
				if (Parameter.IsDefined(typeof(InstanceKeyAttribute)))
				{
					token = Parameter.GetCustomAttribute<InstanceKeyAttribute>().GetKey();
				}
				Params[i] = GetService(Parameter.ParameterType,token);
				i++;
			}
			return Params;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Type"></param>
		/// <param name="o"></param>
		private void CreateProperties(Type Type,object o,Type AutowiredType)
		{
			foreach (var Property in Type.GetProperties(
				BindingFlags.NonPublic 
				| BindingFlags.Instance
				|BindingFlags.Public).
				Where(p=>p.IsDefined(AutowiredType)))
			{
				string token=string.Empty;
				if (Property.IsDefined(typeof(InstanceKeyAttribute)))
				{
					token = Property.GetCustomAttribute<InstanceKeyAttribute>().GetKey();
				}
				Property.SetValue(o,GetService(Property.PropertyType,token));
			}
		}

		private TypeMapper GetTypeMapper(Type Original,Type target)
		{
			TypeMapper typeMapper = null;
			if (typeMapperDic.ContainsKey(Original.FullName))
			{
				typeMapper = typeMapperDic[Original.FullName];
				typeMapper.AddTarget(target);
			}
			return typeMapper;
		}

		private TypeMapper RegisterReturnTypeMapper(Type originalType, Type targetType)
		{
			TypeMapper typeMapper = GetTypeMapper(originalType, targetType);
			if (typeMapper == null)
			{
				typeMapper = new TypeMapper(originalType, targetType);
			}
			Register(typeMapper);
			return typeMapper;
		}

		private TypeMapper RegisterReturnTypeMapper(Type targetType)
		{
			TypeMapper typeMapper = new TypeMapper(targetType);
			Register(typeMapper);
			return typeMapper;
		}

	}
}
