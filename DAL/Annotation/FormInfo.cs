using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain.Resources;

namespace Domain
{
    [MetadataType(typeof(FormInfoAnnotations))]
    public partial class FormInfo
    {
    }

    public class FormInfoAnnotations
    {
        public int Id { get; set; }
        [Display(Name = "Label_FormInfoName", ResourceType = typeof(Messages))]
        public string Name { get; set; }
        [Display(Name = "Label_FormInDescription", ResourceType = typeof(Messages))]
        public string Description { get; set; }
        public virtual ICollection<FormAccess> FormAccesses { get; set; }

    }
}
