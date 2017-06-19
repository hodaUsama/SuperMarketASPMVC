using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        TEntity Get(params object [] id);
        TEntity Add(TEntity tentity);
        bool Update(TEntity tentity);
        bool  Delete(TEntity tentity);

    }
}
