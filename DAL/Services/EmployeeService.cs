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
  public   class EmployeeService : Repository<Employee>
    {
        public EmployeeService()
        { }

        public EmployeeService(DbContext _DbContext) : base(_DbContext)
        {

        }

        public override bool Delete(Employee tentity)
        {
            return base.Delete(tentity);
        }

        public override Employee Add(Employee tentity)
        {
            return base.Add(tentity);
        }

        public override Employee Get(params object[] id)
        {
            return base.Get(id);
        }

        public override bool Update(Employee tentity)
        {
            return base.Update(tentity);
        }

    }
}
