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
    public class RoleServices : Repository<Role>
    {
        public RoleServices()
        { }

        public RoleServices(DbContext _DbContext) : base(_DbContext)
        {

        }

        public override bool Delete(Role tentity)
        {
            return base.Delete(tentity);
        }

        public override Role Add(Role tentity)
        {
            return base.Add(tentity);
        }

        public override Role Get(params object[] id)
        {
            return base.Get(id);
        }

        public override bool Update(Role tentity)
        {
            return base.Update(tentity);
        }

        public void Update(Role rol, List<User> lstUsrs)
        {
            var existingParent = base.Context.Set<Role>().Where(p => p.Id == rol.Id)
                .Include(p => p.Users).Include(p => p.FormAccesses).SingleOrDefault();

            if (existingParent != null)
            {
                // Update parent
                base.Context.Entry(existingParent).CurrentValues.SetValues(rol);

                // Delete Old Users
                foreach (var existingChild in existingParent.Users.ToList())
                {
                    IObjectContextAdapter contextAdapter = (IObjectContextAdapter)base.Context;
                    System.Data.Entity.Core.Objects.ObjectStateManager stateManager = contextAdapter.
                        ObjectContext.ObjectStateManager;
                    stateManager.ChangeRelationshipState(existingParent, existingChild, "Users", EntityState.Deleted);
                }
                //Add New Users
                List<User> lstUsers = new List<User>();
                foreach (User usr in lstUsrs)
                {
                    lstUsers.Add((User)base.Context.Set(typeof(User)).Find(usr.Id));
                }
                base.Context.Entry(existingParent).Collection("Users").CurrentValue = lstUsers;
                base.Context.SaveChanges();
            }
        }

        public Role Add(Role rol, List<User> lstUser)
        {            
            //this is model validation
            if (!Validator.IsValid(rol))
                throw new Exception();//getvalidationerrrors

            base.Context.Set<Role>().Add(rol);
            foreach (User r in lstUser)
            {
                base.Context.Set<User>().Attach(r);
            }
            rol.Users = lstUser;
            base.Context.SaveChanges();
            return rol;
        }

    }
}

