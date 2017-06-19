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
    public class FormAccessService : Repository<FormAccess>
    {
        public FormAccessService()
        { }

        public FormAccessService(DbContext _DbContext) : base(_DbContext)
        {

        }

        public override bool Delete(FormAccess tentity)
        {
            return base.Delete(tentity);
        }

        public override FormAccess Add(FormAccess tentity)
        {
            return base.Add(tentity);
        }

        public override FormAccess Get(params object[] id)
        {
            return base.Get(id);
        }

        public override bool Update(FormAccess tentity)
        {
            return base.Update(tentity);
        }

    }
}
