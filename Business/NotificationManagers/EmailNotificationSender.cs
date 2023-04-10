using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business.NotificationManagers
{
    public class EmailNotificationSender : INotificationSender
    {
        public void Send(string text)
        {
            
            var from = "acerpro.test@outlook.com";
            var password = "acerpro123!";

            var to = "alperentopcuoglu@hotmail.com";
            var subject = "App Down";

            var client = new SmtpClient("smtp-mail.outlook.com",587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential (from,password)
            };

            client.Send(new MailMessage(from, to, subject, text));
        }
    }
}
