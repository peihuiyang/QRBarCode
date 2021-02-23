using Microsoft.AspNetCore.Mvc.Filters;
using Peihui.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRBarCode.Api.Configuration.Filters
{
    /// <summary>
    /// 简单的权限过滤
    /// </summary>
    public class SimpleAuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // 获取接口的Token值
            string token = context.HttpContext.Request.Headers["Authorization"].ToString();
            // 获取配置文件的权限码
            string fucCode = JsonConfigHelper.GetSectionValue("Token");
            if (token != fucCode || token == null)
            {
                throw new Exception("权限不足！");
            }
        }
    }
}
