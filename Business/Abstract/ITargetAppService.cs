using Core.Utilities.Results;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITargetAppService
    {
        IDataResult<List<TargetAppDto>> GetList();
        IDataResult<TargetAppDto> GetById(int id);
        IResult Add(TargetAppDto model);
        IResult Edit(TargetAppDto model);

        IResult Delete(int id);

    }
}
