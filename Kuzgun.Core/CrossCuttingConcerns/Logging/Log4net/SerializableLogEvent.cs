using System;
using System.Collections.Generic;
using System.Text;
using log4net.Core;

namespace Kuzgun.Core.CrossCuttingConcerns.Logging.Log4net
{
    [Serializable]
    public class SerializableLogEvent
    {
        private LoggingEvent _loggingEvent;

        public SerializableLogEvent(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;
        }

        public object Message => _loggingEvent.MessageObject;
    }
}
