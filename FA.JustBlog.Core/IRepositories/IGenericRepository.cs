using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.IRepositories
{
    public interface IGenericRepository<TEntity>: IDisposable where TEntity: class
    {
        IList<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void DeleteById(int entityId);
        TEntity FindById(int entityId);
    }
}
