using Business.Abstract.Base;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Business.Concrete.Base
{
    public class BusinessServiceBase : IServiceBase
    {
        protected readonly IUnitOfWork _repository;

        public BusinessServiceBase(IUnitOfWork repository )
        {
            _repository = repository;
        
        }

    }
}
