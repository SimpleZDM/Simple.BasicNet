using Simple.BasicNet.Core.Atrributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Container
 * 接口名称 TypeMapping
 * 开发人员：11920
 * 创建时间：2023/4/6 13:02:08
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core
{
	internal class TypeMapper:ITypeMapper
	{
	    public TypeMapper(Type original,Type target,eLifeCycle lifeCycle)
		{
			Original= original;
			Target = new Dictionary<string,Tuple<Type, object>>();
			AddTarget(target);
			LifeCycle= lifeCycle;
			IsAutowired = false;
			AutowiredType = typeof(AutowiredAttribute);
		}

		public TypeMapper(Type original,Type target):this(original, target, eLifeCycle.Instant)
		{
		}

		public TypeMapper(Type target,eLifeCycle lifeCycle):this(target,target,lifeCycle)
		{

		}
		public TypeMapper(Type target) : this(target,eLifeCycle.Instant)
		{
		}

		public Type Original { get { return original; } set { original = value; } }
		public Dictionary<string,Tuple<Type,object>> Target { get { return target; } set { target = value; } }
		public eLifeCycle LifeCycle { get { return lifeCycle; } set { lifeCycle = value; } }
		public string TargetKey { get {return targetKey; } set { targetKey = value; } }
		public bool IsAutowired { get {return isAutowired; } set { isAutowired = value; } }
		public Type AutowiredType { get {return autowiredType; } set { autowiredType = value; } }

		private Type original;
		private Dictionary<string,Tuple<Type,object>> target;
		private eLifeCycle lifeCycle;
		private string targetKey;
		private bool isAutowired;
		private Type autowiredType;

		public void AddTarget(Type target)
		{
			if (!Target.ContainsKey(target.FullName))
			{
				TargetKey= target.FullName;
				Target.Add(TargetKey,Tuple.Create<Type,object>(target,null));
			}
		}
		public string GetTypeMapperKey()
		{
			return $"{Original.FullName}";
		}
		public void Verify()
		{
			if (!GetTargetType(TargetKey).IsAssignableTo(Original)
				&& !GetTargetType(TargetKey).GetInterfaces().Any(t => t.FullName == Original.FullName))
				throw new Exception("注册失败,TTarget类型必须继承或者实现Original类型!");
		}
		public Type GetTargetType(string key)
		{
			if (Target.ContainsKey(key))
			{
				return Target[key].Item1;
			}
			return default(Type);
		}
		public object GetInstance(string key)
		{
			if (Target.ContainsKey(key))
			{
				return Target[key].Item2;
			}
			return default(object);
		}
		public void SetInstance(string key,object target)
		{
			if (Target.ContainsKey(key))
			{
				Target[key]=Tuple.Create<Type,object>(target.GetType(),target);
			}
		}
		#region 
		public void Autowired()
		{
			this.isAutowired= true;
		}
		public void Autowired(Type type)
		{
			autowiredType=type;
			Autowired();
		}
		#endregion
	}
}
