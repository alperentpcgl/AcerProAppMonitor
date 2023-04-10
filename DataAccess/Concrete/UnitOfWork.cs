
using DataAccess.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ApplicationDbContext appDbContext,
            ITargetAppRepository targetAppRepository)
        {
            _dbContext = appDbContext;
            TargetApp = targetAppRepository;

        }

        public ApplicationDbContext _dbContext { get; }
        public ITargetAppRepository TargetApp { get; }

        public void ExecuteSqlRaw(string sqlCommand)
        {
            _dbContext.Database.ExecuteSqlRaw(sqlCommand);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }


        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
