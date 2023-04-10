using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Results;
using DataAccess;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Model;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.JobSchedulerManagers
{
    public class JobSchedulerManager : IJobSchedulerManager
    {
        private readonly IUnitOfWork _repository;
        public JobSchedulerManager(IUnitOfWork repository)
        {
            _repository = repository;
        }
        public void Start()
        {
            try
            {
                var schedContext = new StdSchedulerFactory();
                var scheduler = schedContext.GetScheduler();

                if (!scheduler.Result.IsStarted)
                {
                    scheduler.Result.Start();
                }


                var jobList = _repository.TargetApp.GetAllList();

                foreach (var jobItem in jobList)
                {


                    IJobDetail job = JobBuilder.Create<Job>().WithIdentity(jobItem.Name, "group1")
                        .UsingJobData("Url", jobItem.Url).Build();

                    ITrigger trigger = TriggerBuilder.Create().StartNow().WithDescription(jobItem.Name)
                       .WithSimpleSchedule(x => x.RepeatForever()
                                                .WithIntervalInSeconds(jobItem.Interval)
                                        ).Build();

                    scheduler.Result.ScheduleJob(job, trigger);


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public IResult ResetAllJob()
        {

            var schedContext = new StdSchedulerFactory();
            var scheduler = schedContext.GetScheduler();

            scheduler.Result.Shutdown();

            Start();

            return new Result(true);
        }

        public IResult ReCreateJob(string oldName,TargetAppDto model)
        {
            var schedContext = new StdSchedulerFactory();
            var scheduler = schedContext.GetScheduler();

            var result = scheduler.Result.DeleteJob(new JobKey(oldName, "group1")).Result;
            if(!result)
                return new Result(false);

            IJobDetail newjob = JobBuilder.Create<Job>().WithIdentity(model.Name, "group1")
               .UsingJobData("Url", model.Url).Build();

            ITrigger newtrigger = TriggerBuilder.Create().StartNow().WithDescription(model.Name)
               .WithSimpleSchedule(x => x.RepeatForever()
                                        .WithIntervalInSeconds(model.Interval)
                                ).Build();

            scheduler.Result.ScheduleJob(newjob, newtrigger);

            return new Result(true);
        }

        public IResult AddJob(TargetAppDto model)
        {
            var schedContext = new StdSchedulerFactory();
            var scheduler = schedContext.GetScheduler();

            IJobDetail job = JobBuilder.Create<Job>().WithIdentity(model.Name, "group1")
                       .UsingJobData("Url", model.Url).Build();

            ITrigger trigger = TriggerBuilder.Create().StartNow().WithDescription(model.Name)
               .WithSimpleSchedule(x => x.RepeatForever()
                                        .WithIntervalInSeconds(model.Interval)
                                ).Build();

            scheduler.Result.ScheduleJob(job, trigger);

            return new Result(true);
        }

        public IResult RemoveJob(string name)
        {
            var schedContext = new StdSchedulerFactory();
            var scheduler = schedContext.GetScheduler();

            var result = scheduler.Result.DeleteJob(new JobKey(name, "group1")).Result;

            return new Result(result);

        }
    }
}
