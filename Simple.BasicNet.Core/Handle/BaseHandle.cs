﻿using Simple.BasicNet.Core.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 Simple.BasicNet.Core
 * 接口名称 BaseHandle
 * 开发人员：11920
 * 创建时间：2023/4/6 10:15:27
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace Simple.BasicNet.Core.Handle
{
    public class BaseHandle
    {
        protected HandleContext context;
        public BaseHandle()
        {

        }

        public void SetContext(HandleContext context)
        {
            if (this.context!=null)
                throw new Exception("处理上下文不允许修改!");
            this.context = context;
        }
    }
}
