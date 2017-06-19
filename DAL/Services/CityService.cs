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
  public class CityService : Repository<City>
    {
        public CityService()
        { }

        public CityService(DbContext _DbContext) : base(_DbContext)
        {

        }

        public override bool Delete(City tentity)
        {
            return base.Delete(tentity);
        }

        public override City Add(City tentity)
        {
            return base.Add(tentity);
        }

        public override City Get(params object[] id)
        {
            return base.Get(id);
        }

        public override bool Update(City tentity)
        {
            return base.Update(tentity);
        }

        public IQueryable<City> GetCitiesByCountry(int CountryId)
        {
            return base.Context.Set<City>().Where(x => x.CountryId == CountryId);
        }

        public IQueryable<State> Getstatebycity(int CityId)
        {
            return base.Context.Set<State>().Where(x => x.CityId == CityId);
        }
    }
}
