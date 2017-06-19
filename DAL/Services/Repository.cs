
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
   public class Repository<t> : IRepository<t> where t :class
    {
        private DbContext _Context;
        private DbSet _dbset;
        public Repository():this(new Context())
        { }
        public  Repository(DbContext dbcont)
        {
            _Context = dbcont;
            _dbset = Context.Set<t>();
        }
        public virtual DbContext Context
        {
            get
            {
                return _Context;
            }            
        }
        public virtual t Add(t tentity)
        {            
            t result = null;
            //this is model validation
            if (!Validator.IsValid(tentity))
                throw new Exception();//getvalidationerrrors

            _dbset.Add(tentity);
            if(Context.SaveChanges()>0)
            {
                result = tentity;
            }
            return result;
        }
        public virtual bool Delete(t tentity)
        {
            Context.Entry<t>(tentity).State = System.Data.Entity.EntityState.Deleted;
            return Context.SaveChanges() > 0;
        }
        public virtual t Get(params object[] id)
        {
            return (t) _dbset.Find(id);
        }
        public virtual IQueryable<t> GetAll()
        {
            return (IQueryable<t>) _dbset;
        }
        public virtual bool  Update(t tentity)
        {
            if (!Validator.IsValid(tentity))
                throw new Exception();//getvalidationerrrors
            
            Context.Entry<t>(tentity).State = EntityState.Modified;
            return Context.SaveChanges() > 0;
        }
    }
}
