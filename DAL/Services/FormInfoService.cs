using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;

namespace Domain
{
    public class FormInfoService : Repository<FormInfo>
    {

        public FormInfoService()
        { }

        public FormInfoService(DbContext _DbContext) : base(_DbContext)
        {

        }

        public override bool Delete(FormInfo tentity)
        {
            return base.Delete(tentity);
        }

        public override FormInfo Add(FormInfo tentity)
        {
            return base.Add(tentity);
        }

        public override FormInfo Get(params object[] id)
        {
            return base.Get(id);
        }

        public override bool Update(FormInfo tentity)
        {
            return base.Update(tentity);
        }
    }
}
