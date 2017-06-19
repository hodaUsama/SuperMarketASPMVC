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
 public  class JobTitleService : Repository<JobTitle>
    {
        public JobTitleService()
        { }

        public JobTitleService(DbContext _DbContext) : base(_DbContext)
        {

        }

        public override bool Delete(JobTitle tentity)
        {
            return base.Delete(tentity);
        }

        public override JobTitle Add(JobTitle tentity)
        {
            return base.Add(tentity);
        }

        public override JobTitle Get(params object[] id)
        {
            return base.Get(id);
        }

        public override bool Update(JobTitle tentity)
        {
            return base.Update(tentity);
        }

    }
}