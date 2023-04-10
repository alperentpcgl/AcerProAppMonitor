using Core.Utilities.Results;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.JobSchedulerManagers
{
    public interface IJobSchedulerManager
    {
        void Start();
        IResult ResetAllJob();
        IResult AddJob(TargetAppDto model);
        IResult RemoveJob(string name);
        IResult ReCreateJob(string oldName, TargetAppDto model);


    }
}
