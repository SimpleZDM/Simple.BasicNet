using Simple.BasicNet.Core.Atrributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Container
 * 接口名称 ContainerExternal
 * 开发人员：11920
 * 创建时间：2023/4/10 9:22:35
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core
{
	internal class TypeMapperExternal: ITypeMapperExternal
	{
		public static ITypeMapperExternal Create(params TypeMapper[] typeMappers)
		{
			return new TypeMapperExternal(typeMappers);
		}
		public static ITypeMapperExternal Create(List<TypeMapper>typeMappers)
		{
			return Create(typeMappers);
		}
		private TypeMapper[] typeMappers; 
		public TypeMapperExternal(params TypeMapper [] typeMappers) 
		{
			this.typeMappers= typeMappers;
		}
		public void Autowird(Type type)
		{
			foreach (var item in typeMappers)
			{
				item.AutowiredType = type;
				item.IsAutowired= true;
			}
		}
		public void Autowird()
		{
			foreach (var item in typeMappers)
			{
				item.IsAutowired = true;
			}
		}
	}
}
