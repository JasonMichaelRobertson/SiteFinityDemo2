using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SitefinityWebApp.Utilities
{
    public static class Email
    {
        public static bool SendEmail(MailMessage m_message)
        {
            bool m_status = false;
            try
            {
                SmtpClient m_client = new SmtpClient();
                m_client.Host = Settings.SMTPServerName();
                m_client.Port = Settings.SMTPPortNo();
                m_client.Send(m_message);
                m_status = true;
            }
            catch (SmtpException ex)
            {
                string m_error = ex.Message;
                throw;
            }
            return m_status;
        }
    }
}