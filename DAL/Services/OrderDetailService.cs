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
  public  class OrderDetailService : Repository<OrderDetail>
    {
        public OrderDetailService()
        { }

        public OrderDetailService(DbContext _DbContext) : base(_DbContext)
        {

        }

        public override bool Delete(OrderDetail tentity)
        {
            return base.Delete(tentity);
        }

        public override OrderDetail Add(OrderDetail tentity)
        {
            return base.Add(tentity);
        }

        public override OrderDetail Get(params object[] id)
        {
            return base.Get(id);
        }

        public override bool Update(OrderDetail tentity)
        {
            return base.Update(tentity);
        }

    }
}