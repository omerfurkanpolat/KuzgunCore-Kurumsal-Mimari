using System;
using System.Collections.Generic;
using System.Text;

namespace Kuzgun.Core.Utilities.EmailService.Smtp
{
    public interface IEmailService
    {
        void SendMail(string userName, string email, string subjectText, string callBackUrl);
    }
}
