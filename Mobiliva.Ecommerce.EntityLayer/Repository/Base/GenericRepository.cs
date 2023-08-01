using Mobiliva.Ecommerce.Data.Context;
using Mobiliva.Ecommerce.Data.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Mobiliva.Ecommerce.Data.Repository.Base
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseModel
    {
        protected readonly EcommerceEfDbContext dbContext;
        private bool disposed;

        public GenericRepository(EcommerceEfDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Delete(Entity entity)
        {
            dbContext.Set<Entity>().Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = dbContext.Set<Entity>().Find(id);
            dbContext.Set<Entity>().Remove(entity);
        }

        public List<Entity> GetAll()
        {
            return dbContext.Set<Entity>().ToList();
        }
        public List<Entity> GetAllAsNoTracking()
        {
            return dbContext.Set<Entity>().AsNoTracking().ToList();
        }
        public List<Entity> GetAllWithInclude(params string[] includes)
        {
            var query = dbContext.Set<Entity>().AsQueryable();
            query = includes.Aggregate(query, (current, inc) => current.Include(inc));
            return query.ToList();
        }
        public Entity GetByIdWithInclude(int id, params string[] includes)
        {
            var query = dbContext.Set<Entity>().AsQueryable();
            query = includes.Aggregate(query, (current, inc) => current.Include(inc));
            return query.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Entity> WhereWithInclude(Expression<Func<Entity, bool>> expression, params string[] includes)
        {
            var query = dbContext.Set<Entity>().AsQueryable();
            query.Where(expression);
            query = includes.Aggregate(query, (current, inc) => current.Include(inc));
            return query.ToList();
        }

        public IQueryable<Entity> GetAsQueryable()
        {
            return dbContext.Set<Entity>().AsQueryable();
        }
        public Entity GetById(int id)
        {
            return dbContext.Set<Entity>().Find(id);
        }
        public Entity GetByIdAsNoTracking(int id)
        {
            return dbContext.Set<Entity>().AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Entity entity)
        {
            dbContext.Set<Entity>().Add(entity);
        }

        public void Update(Entity entity)
        {
            dbContext.Set<Entity>().Update(entity);
        }

        public IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression)
        {
            return dbContext.Set<Entity>().Where(expression).AsQueryable();
        }
        public IEnumerable<Entity> WhereAsNoTracking(Expression<Func<Entity, bool>> expression)
        {
            return dbContext.Set<Entity>().AsNoTracking().Where(expression).AsQueryable();
        }

        public void Complete()
        {
            dbContext.SaveChanges();
        }

        public void CompleteWithTransaction()
        {
            using (var dbDcontextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.SaveChanges();
                    dbDcontextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    // logging
                    dbDcontextTransaction.Rollback();
                }
            }
        }


        private void Clean(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }

            disposed = true;
            GC.SuppressFinalize(this);
        }
        public void Dispose()
        {
            Clean(true);
        }

    }
}
