using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.NotificationManagers
{
    public interface INotificationSender
    {
        void Send(string text);
    }
}
