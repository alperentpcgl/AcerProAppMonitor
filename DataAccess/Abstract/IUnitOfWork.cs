using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUnitOfWork: IDisposable
    {
        IDbContextTransaction BeginTransaction();

        ITargetAppRepository TargetApp { get; }

        void ExecuteSqlRaw(string sqlCommand);
        Task<int> CommitAsync();
        int Commit();
    }
}
