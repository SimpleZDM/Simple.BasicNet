using Simple.BasicNet.Core.Atrributes;
using Simple.BasicNet.Core.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core
 * 接口名称 FindBaseHandleChildren
 * 开发人员：11920
 * 创建时间：2023/4/6 10:17:48
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core
{
    public class FindBaseHandleChildren
	{
		public FindBaseHandleChildren()
		{

		}
		public void Find(int targetNumber)
		{
			Type type = typeof(BaseHandle);
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (var assembliy in assemblies)
			{
				foreach (var itemType in assembliy.GetTypes())
				{
					if (itemType.IsAssignableTo(type))
					{

						if (itemType.IsDefined(typeof(NumberRangeAttribute), true))
						{

							NumberRangeAttribute range = itemType.GetCustomAttribute<NumberRangeAttribute>();
							if (targetNumber <= range.MaxMessageNumber && targetNumber >= range.MinMessageNumber)
							{
								object o=Activator.CreateInstance(itemType);
								foreach (var method in itemType.GetMethods().Where(m => m.IsDefined(typeof(MessageNumberAttribute))))
								{
									MessageNumberAttribute number= method.GetCustomAttribute<MessageNumberAttribute>();
									if (number.Number==targetNumber)
									{
										method.Invoke(o,new object[]{});
									}

								}

								//Console.WriteLine($"typeName:{itemType.Name}");
								//Console.WriteLine($"full typeName:{itemType.FullName}");
							}
						}
					}
				}
			}
		}
	}
}
