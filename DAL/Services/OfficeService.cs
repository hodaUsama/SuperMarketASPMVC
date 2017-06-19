using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Domain
{
   public  class OfficeService : Repository<Office>
    {
        public OfficeService()
        { }

        public OfficeService(DbContext _DbContext) : base(_DbContext)
        {

        }

        public override bool Delete(Office tentity)
        {
            return base.Delete(tentity);
        }

        public override Office Add(Office tentity)
        {
            return base.Add(tentity);
        }

        public override Office Get(params object[] id)
        {
            return base.Get(id);
        }

        public override bool Update(Office tentity)
        {
            return base.Update(tentity);
        }

    }
}