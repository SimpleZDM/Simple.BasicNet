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

		public void Register<TOriginal,TTarget>()
		{
			Register(typeof(TOriginal), typeof(TTarget));
		}
		public void Register<TTarget>()
		{
			Register(typeof(TTarget));
		}

		public void Register(Type targetType)
		{
			TypeMapper typeMapper = new TypeMapper(targetType);
			typeMapper.Verify();
			string key = typeMapper.GetTypeMapperKey();
			if (!typeMapperDic.ContainsKey(key))
			{
				typeMapperDic.TryAdd(key, typeMapper);
			}
		}

		public void Register(Type [] targetTypes)
		{
			foreach (var item in targetTypes)
			{
				Register(item);
			}
		}

		public void Register(Type originalType, Type targetType)
		{
			TypeMapper typeMapper = new TypeMapper(originalType,targetType);
			typeMapper.Verify();
			string key = typeMapper.GetTypeMapperKey();
			if (!typeMapperDic.ContainsKey(key))
			{
				typeMapperDic.TryAdd(key, typeMapper);
			}
		}

		/// <summary>
		/// 接口和实现
		/// </summary>
		/// <param name="original"></param>
		/// <param name="target"></param>
		public void RegisterAssambly(Assembly original,Assembly target)
		{

		}
		public Target GetService<Target>()
		{
			Type targetType= typeof(Target);
			return (Target)GetService(targetType);
		}
		public object GetService(Type target)
		{
			object o = null;
			if(typeMapperDic.Where(mapper =>
				mapper.Value.Target.FullName == target.FullName
			|| mapper.Value.Original.FullName == target.FullName).Any())
			{
				var typeMapper = typeMapperDic.FirstOrDefault(mapper => 
				mapper.Value.Target.FullName == target.FullName
				|| mapper.Value.Original.FullName == target.FullName).Value;
				//单例就返回
				if (typeMapper.LifeCycle==eLifeCycle.Single)
				{
					return typeMapper.Instance;
				}
				var Constructor = typeMapper.Target.GetConstructors().FirstOrDefault();
				if (Constructor == null)
					throw new Exception("获取构造方法失败,请检查是否将接口注入到容器中,接口不具有构造方式!");
				
				object[] Params = new object[Constructor.GetParameters().Count()];
				int i = 0;
					foreach (var Parameter in Constructor.GetParameters())
					{
						Params[i] = GetService(Parameter.ParameterType);
						i++;
					}
					o = Activator.CreateInstance(typeMapper.Target, Params);
			}

			return o;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="typeMapper"></param>
		/// <returns></returns>
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="typeMapper"></param>
		/// <exception cref="Exception"></exception>
		
	}
}
