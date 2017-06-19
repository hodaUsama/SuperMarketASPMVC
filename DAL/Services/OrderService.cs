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
   public class OrderService : Repository<Order>
    {
        public OrderService()
        { }

        public OrderService(DbContext _DbContext) : base(_DbContext)
        {

        }

        public override bool Delete(Order tentity)
        {
            return base.Delete(tentity);
        }

        public override Order Add(Order tentity)
        {
            return base.Add(tentity);
        }

        public override Order Get(params object[] id)
        {
            return base.Get(id);
        }

        public override bool Update(Order tentity)
        {
            return base.Update(tentity);
        }

    }
}