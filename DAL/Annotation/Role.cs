using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain.Resources;

namespace Domain
{
    [MetadataType(typeof(RoleAnnotations))]
    public partial class Role
    {

    }
    public class RoleAnnotations
    {
        public int Id { get; set; }

        [Display(Name = "Label_RoleName", ResourceType = typeof(Messages))]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Label_RoleDescription", ResourceType = typeof(Messages))]
        [StringLength(50)]
        public string Description { get; set; }
        public virtual ICollection<FormAccess> FormAccesses { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
