using log4net;
using log4net.Config;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QRBarCode.Api.Configuration.CustLog
{
    /// <summary>
    /// 日志拓展类
    /// </summary>
    public static class Log4NetExtend
    {
        public static IHostBuilder UseLog4Net(this IHostBuilder hostBuilder)
        {
            var log4netRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(log4netRepository, new FileInfo("Log4Net.config"));
            return hostBuilder;
        }
    }
}
