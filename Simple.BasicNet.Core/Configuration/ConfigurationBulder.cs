using Newtonsoft.Json;
using Simple.BasicNet.Core.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core.Configuration
 * 接口名称 ConfigurationBulder
 * 开发人员：11920
 * 创建时间：2023/4/7 14:23:33
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Configuration
{
	internal class ConfigurationBulder
	{
		private string strJsonCfg;
		public ConfigurationBulder(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new Exception("指定配置文件不存在!");
			}
			strJsonCfg = File.ReadAllText(filePath);
		}

		public ServiceConfigution GetConfigution()
		{
				if (string.IsNullOrEmpty(strJsonCfg))
					throw new Exception("配置文件错误!");
			return JsonConvert.DeserializeObject<ServiceConfigution>(strJsonCfg);
		}
	}
}
