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
    public class UserServices : Repository<User>
    {
        public UserServices()
        { }

        public UserServices(DbContext _DbContext) : base(_DbContext)
        {

        }

        public override bool Delete(User tentity)
        {
            return base.Delete(tentity);
        }

        public override User Add(User tentity)
        {
            return base.Add(tentity);
        }

        public override User Get(params object[] id)
        {
            return base.Get(id);
        }

        public override bool Update(User tentity)
        {
            return base.Update(tentity);
        }

        public void Update(User usr, int[] lstRol)
        {
            var existingParent = base.Context.Set<User>().Where(p => p.Id == usr.Id)
                .Include(p => p.Roles).SingleOrDefault();

            if (existingParent != null)
            {
                // Update parent
                base.Context.Entry(existingParent).CurrentValues.SetValues(usr);

                // Delete Old children
                foreach (var existingChild in existingParent.Roles.ToList())
                {
                    IObjectContextAdapter contextAdapter = (IObjectContextAdapter)base.Context;
                    System.Data.Entity.Core.Objects.ObjectStateManager stateManager = contextAdapter.
                        ObjectContext.ObjectStateManager;
                    stateManager.ChangeRelationshipState(existingParent, existingChild, "Roles", EntityState.Deleted);
                }
                List<Role> lstRoles = new List<Role>();
                foreach (int id in lstRol)
                {
                    lstRoles.Add((Role)base.Context.Set(typeof(Role)).Find(id));
                }
                base.Context.Entry(existingParent).Collection("Roles").CurrentValue = lstRoles;
                base.Context.SaveChanges();
            }
        }

        public User Add(User usr, List<Role> lstRol)
        {
            //this is model validation
            if (!Validator.IsValid(usr))
                throw new Exception();//getvalidationerrrors

            base.Context.Set<User>().Add(usr);
            foreach (Role r in lstRol)
            {
                base.Context.Set<Role>().Attach(r);
            }
            usr.Roles = lstRol;
            base.Context.SaveChanges();
            return usr;
        }



    }
}
