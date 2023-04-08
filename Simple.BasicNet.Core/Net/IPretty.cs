using Simple.BasicNet.Core.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Simple.BasicNet.Core.Net
{
	public interface IPretty
	{
        public void Start();

        public void Initalization();
        
        public void Accept();

    }
}
