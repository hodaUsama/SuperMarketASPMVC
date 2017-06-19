using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain.Resources;

namespace Domain
{
    [MetadataType(typeof(FormAccessAnnotations))]
    public partial class FormAccess
    {

    }
    public class FormAccessAnnotations
    {
        public int Id { get; set; }

        [Display(Name = "Ddl_Form", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "DDl_Form_error", ErrorMessageResourceType = typeof(Messages))]
        public int FormIdFK { get; set; }

        [Display(Name = "Label_Read", ResourceType = typeof(Messages))]
        public Nullable<bool> ReadForm { get; set; }
        [Display(Name = "Label_Create", ResourceType = typeof(Messages))]
        public Nullable<bool> createForm { get; set; }
        [Display(Name = "Label_Edit", ResourceType = typeof(Messages))]
        public Nullable<bool> EditForm { get; set; }
        [Display(Name = "Label_Delete", ResourceType = typeof(Messages))]
        public Nullable<bool> DeleteForm { get; set; }

        [Display(Name = "Ddl_Role", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "DDl_Role_error", ErrorMessageResourceType = typeof(Messages))]
        public int RoleidFK { get; set; }

    }
}
