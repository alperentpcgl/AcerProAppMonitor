using Business.Abstract;
using Business.Concrete.Base;
using Business.JobSchedulerManagers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity;
//using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TargetAppService : BusinessServiceBase, ITargetAppService
    {
        private readonly IJobSchedulerManager _jobSchedulerManager;
        public TargetAppService(IUnitOfWork repository, IJobSchedulerManager jobSchedulerManager) : base(repository)
        {
            _jobSchedulerManager = jobSchedulerManager;
        }

        public IDataResult<List<TargetAppDto>> GetList()
        {
            var data=_repository.TargetApp.GetAll().Select(x => new TargetAppDto {
                Id=x.Id,
                Name=x.Name,
                Interval=x.Interval,
                Url=x.Url
                }).ToList();

            return new SuccessDataResult<List<TargetAppDto>>(data);
        }

        public IDataResult<TargetAppDto> GetById(int id)
        {
            var data = _repository.TargetApp.Where(x => x.Id == id).Select(x => new TargetAppDto {
                Id = x.Id,
                Name = x.Name,
                Interval = x.Interval,
                Url = x.Url
            }).FirstOrDefault();

            return new SuccessDataResult<TargetAppDto>(data);
        }

        public IResult Add(TargetAppDto model)
        {

            var entity = new TargetApp
            {
                Name=model.Name,
                Interval=model.Interval,
                Url = model.Url
            };
            _repository.TargetApp.InsertAsQuery(entity);

            var result=_jobSchedulerManager.AddJob(model);
            if(result.Success)
                _repository.Commit();

            return new SuccessResult();
        }
        public IResult Edit(TargetAppDto model)
        {
            var oldName = string.Empty;
            var entity = _repository.TargetApp.Get(model.Id);

            oldName = entity.Name;

            entity.Name=model.Name;
            entity.Interval=model.Interval;
            entity.Url=model.Url;
            _repository.TargetApp.Update(entity);

            var result=_jobSchedulerManager.ReCreateJob(oldName,model);
            if (result.Success)
                _repository.Commit();

            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            var entity = _repository.TargetApp.FirstOrDefault(x=>x.Id==id);
            _repository.TargetApp.Delete(entity);

            var result=_jobSchedulerManager.RemoveJob(entity.Name);
            if (result.Success)
                _repository.Commit();

            return new SuccessResult();
        }
    }
}
