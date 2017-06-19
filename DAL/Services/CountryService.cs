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
    public class CountryService : Repository<Country>
    {
        public CountryService()
        { }

        public CountryService(DbContext _DbContext) : base(_DbContext)
        {

        }

        public override bool Delete(Country tentity)
        {
            return base.Delete(tentity);
        }

        public override Country Add(Country tentity)
        {
            return base.Add(tentity);
        }

        public override Country Get(params object[] id)
        {
            return base.Get(id);
        }

        public override bool Update(Country tentity)
        {
            return base.Update(tentity);
        }

    }
}
