using Business.NotificationManagers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Business.JobSchedulerManagers
{
    public class Job : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            NotificationManager _notificationManager = new NotificationManager(new EmailNotificationSender());
            var Name = context.JobDetail.Key.Name;
            var Group = context.JobDetail.Key.Group;

            JobDataMap dataMap = context.JobDetail.JobDataMap;

            string Url = dataMap.GetString("Url");

            // 10 saniyeyi geçerse timeout app is down
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            bool isSuccessStatusCode = true;
            try
            {
                var checkingResponse = await client.GetAsync(Url);

                isSuccessStatusCode = checkingResponse.IsSuccessStatusCode;
               

            }
            catch (Exception ex)
            {
                isSuccessStatusCode = false;
            }
            finally
            {
                if(!isSuccessStatusCode)
                    _notificationManager.SendNotification($"{Name} - {Url} target app is down!");
            }



        }
    }
}
