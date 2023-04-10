using Core.Domain.Entities;
using Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataAccess.Concrete
{

    public class EfRepositoryBase<TEntity> : EfRepositoryBase<TEntity, int>,
       IRepositoryBase<TEntity>
       where TEntity : class, IEntity, new()
    {

        public EfRepositoryBase(ApplicationDbContext context) : base(context)
        {
        }
    }


    public class EfRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>
            , IRepositoryBase<TEntity, TPrimaryKey>
            where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        protected readonly ApplicationDbContext _context;
        public DbSet<TEntity> _entities;
        public EfRepositoryBase(ApplicationDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("dbContext can not be null");
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public override void Delete(TEntity entity)
        {
            var updateEntity = _context.Entry(entity);
            updateEntity.State = EntityState.Deleted;
        }

        public override void Delete(TPrimaryKey id)
        {
            _entities.Remove(this.Get(id));
        }

        public override IQueryable<TEntity> GetAll()
        {
            return _entities;
        }

        public override TEntity Insert(TEntity entity)
        {
            var data = _entities.Add(entity);
            _context.SaveChanges();
            return data.Entity;
        }

        public override TEntity InsertAsQuery(TEntity entity)
        {
            var data = _entities.Add(entity);
            return data.Entity;
        }

        public override TEntity Update(TEntity entity)
        {
            var updateEntity = _context.Entry(entity);
            updateEntity.State = EntityState.Modified;
            return updateEntity.Entity;
        }




    }

}
