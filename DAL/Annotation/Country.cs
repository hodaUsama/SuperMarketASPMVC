using Domain.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Annotation
{

    [MetadataType(typeof(CountryAnnotations))]
    public partial class Country
    {
        
    }

    public class CountryAnnotations
    {
        [Display(Name = "Label_CountryName", ResourceType = typeof(Messages))]
        [StringLength(50)]
        [Required(ErrorMessageResourceName = "Label_Country_Name_Required",
            ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }
    }
}
