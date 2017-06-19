using Domain.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [MetadataType(typeof(UserAnnotations))]
    public partial class User
    {

    }
    public class UserAnnotations
    {
        public int Id { get; set; }

        [Display(Name = "Label_FirstName", ResourceType = typeof(Messages))]
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Label_SecondName", ResourceType = typeof(Messages))]
        [StringLength(50)]
        [Required]
        public string SecondName { get; set; }

        [Display(Name = "Label_UserName", ResourceType = typeof(Messages))]
        [StringLength(50)]
        [Required]
        public string UserName { get; set; }

     
        [Required]
        [Display(Name = "Label_Password", ResourceType = typeof(Messages))]
        [StringLength(100, ErrorMessageResourceName = "Label_Password_error", 
            ErrorMessageResourceType = typeof(Messages), MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Label_DateOfBirth", ResourceType = typeof(Messages))]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [EmailAddress]
        [Required]
        [Display(Name = "Label_Email", ResourceType = typeof(Messages))]
        public string Email { get; set; }

        [Url]
        [Required]
        [Display(Name = "Label_Url", ResourceType = typeof(Messages))]
        public string Url { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
