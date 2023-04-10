using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.NotificationManagers
{
    public class NotificationManager : INotificationManager
    {
        private INotificationSender _sender;
        public NotificationManager(INotificationSender sender)
        {
            _sender=sender;
        }

        public void SendNotification(string text)
        {
            _sender.Send(text);
        }

    }
}
