using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Serilog;

namespace Currency2StringConverterServer
{
    public class Logger
    {
        public static ILogger Log { get; }
        static Logger()
        {
            Log = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();
            Log.Information($"Start logging: {DateTime.Now}");
        }
    }
}