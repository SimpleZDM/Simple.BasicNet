using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.BasicNet.Core
{
	public interface ITypeMapperExternal
	{
		public void Autowird();
		public void Autowird(Type type);
	}
}
