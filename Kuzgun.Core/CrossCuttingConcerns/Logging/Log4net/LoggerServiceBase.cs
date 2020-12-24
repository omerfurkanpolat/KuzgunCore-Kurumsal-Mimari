using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using log4net;
using log4net.Repository;

namespace Kuzgun.Core.CrossCuttingConcerns.Logging.Log4net
{
    public class LoggerServiceBase
    {
        private ILog _log;
        public LoggerServiceBase(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(File.OpenRead("log4net.config"));

            ILoggerRepository loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument["log4net"]);

            _log = LogManager.GetLogger(loggerRepository.Name, name);

        }

        public bool IsInfoEnable => _log.IsInfoEnabled;
        public bool IsIDebugEnable => _log.IsDebugEnabled;
        public bool IsWarnEnable => _log.IsWarnEnabled;
        public bool IsFatalEnable => _log.IsFatalEnabled;
        public bool IsErrorEnable => _log.IsErrorEnabled;

        public void Info(object logMessage)
        {
            if (IsInfoEnable)
            {
                _log.Info(logMessage);
            }

        }
        public void Debug(object logMessage)
        {
            if (IsIDebugEnable)
            {
                _log.Debug(logMessage);
            }

        }
        public void Warn(object logMessage)
        {
            if (IsWarnEnable)
            {
                _log.Warn(logMessage);
            }

        }
        public void Fatal(object logMessage)
        {
            if (IsFatalEnable)
            {
                _log.Fatal(logMessage);
            }

        }
        public void Error(object logMessage)
        {
            if (IsErrorEnable)
            {
                _log.Error(logMessage);
            }

        }

    }
}
