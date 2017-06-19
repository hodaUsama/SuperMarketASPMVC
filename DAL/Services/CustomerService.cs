using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Domain.Services
{
   public  class CustomerService : Repository<Customer>
    {
        public CustomerService()
        { }

        public CustomerService(DbContext _DbContext) : base(_DbContext)
        {

        }

        public override bool Delete(Customer tentity)
        {
            return base.Delete(tentity);
        }

        public override Customer Add(Customer tentity)
        {
            return base.Add(tentity);
        }

        public override Customer Get(params object[] id)
        {
            return base.Get(id);
        }

        public override bool Update(Customer tentity)
        {
            return base.Update(tentity);
        }

    }
}