using System;
using System.Collections.Generic;
using System.Text;

namespace Kuzgun.Core.CrossCuttingConcerns.Logging.Log4net.Loggers
{
    public class FileLogger : LoggerServiceBase
    {

        public FileLogger() : base("JsonFileLogger")
        {
        }
    }
}
