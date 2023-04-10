using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.BasicNet.Core
{
	public interface ITypeMapper
	{
		public void Autowired();
		public void Autowired(Type type);
	}
}
