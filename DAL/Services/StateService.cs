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
   public class StateService : Repository<State>
    {
        public StateService()
        { }

        public StateService(DbContext _DbContext) : base(_DbContext)
        {

        }

        public override bool Delete(State tentity)
        {
            return base.Delete(tentity);
        }

        public override State Add(State tentity)
        {
            return base.Add(tentity);
        }

        public override State Get(params object[] id)
        {
            return base.Get(id);
        }

        public override bool Update(State tentity)
        {
            return base.Update(tentity);
        }

       

    }
}


