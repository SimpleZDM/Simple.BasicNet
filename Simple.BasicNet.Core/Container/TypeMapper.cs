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
	internal class TypeMapper
	{
	    public TypeMapper(Type original,Type target,eLifeCycle lifeCycle)
		{
			Original= original;
			Target= target;
		}

		public TypeMapper(Type original, Type target):this(original, target, eLifeCycle.Instant)
		{
		}

		public TypeMapper(Type target,eLifeCycle lifeCycle) :this(target,target,lifeCycle)
		{

		}
		public TypeMapper(Type target) : this(target,eLifeCycle.Instant)
		{
		}

		public Type Original { get { return original; } set { original = value; } }
		public Type Target { get { return target; } set { target = value; } }
		public eLifeCycle LifeCycle { get { return lifeCycle; } set { lifeCycle = value; } }
		public object Instance { get {return _instance; } set { _instance = value; } }

		private Type original;
		private Type target;
		private eLifeCycle lifeCycle;
		private object _instance;
		public string GetTypeMapperKey()
		{
			if (Original== Target)
			{
				return Target.FullName;
			}
			return $"{Original.FullName}_To_{Target.FullName}";
		}

		public void Verify()
		{
			if (!Target.IsAssignableTo(Original)
				&& !Target.GetInterfaces().Any(t => t.FullName == Original.FullName))
				throw new Exception("注册失败,TTarget类型必须继承或者实现Original类型!");
			var inter = Target.GetInterfaces().FirstOrDefault(t => t.FullName == Original.FullName);
		}
	}
}
