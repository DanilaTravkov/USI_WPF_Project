using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LangLang.Utilities
{
    public class EMail
    {
        private const string EMAIL_USERNAME = "btcnotification036@gmail.com";
        private const string EMAIL_PASSWORD = "xfrv aivd tjmd vzda";

        private string subject;
        private string body;
        private string attachment;

        public EMail(string subject, string body, string attachment)
        {
            this.subject = subject;
            this.body = body;
            this.attachment = attachment;
        }

        public bool Send(string from, string to, string toName)
        {
            if (attachment != "" && !File.Exists(attachment))
            {
                return false;
            }

            try
            {
                var fromAddress = new MailAddress(EMAIL_USERNAME, from);
                var toAddress = new MailAddress(to, toName);

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, EMAIL_PASSWORD)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    if (this.attachment != "")
                    {
                        System.Net.Mail.Attachment attachment;
                        attachment = new System.Net.Mail.Attachment(this.attachment);
                        message.Attachments.Add(attachment);
                    }


                    smtp.Send(message);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
