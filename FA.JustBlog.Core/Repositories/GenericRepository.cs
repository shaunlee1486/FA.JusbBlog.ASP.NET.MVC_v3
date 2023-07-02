using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.JustBlogDbContext;
using System.Data.Entity;

namespace FA.JustBlog.Core.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public readonly JustBlogContext Context;
        private DbSet<TEntity> dbSet;
        public GenericRepository()
        {
            Context = new JustBlogContext();
            dbSet = Context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
            Save();
        }

        public void DeleteById(int entityId)
        {
            var item = dbSet.Find(entityId);
            dbSet.Remove(item);
            Save();
        }

        public TEntity FindById(int entityId)
        {
            return dbSet.Find(entityId);
        }

        public IList<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public void Update(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Modified;
            Save();
        }

        private int Save()
        {
           return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
