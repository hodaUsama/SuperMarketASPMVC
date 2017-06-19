using Domain.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [MetadataType(typeof(CityAnnotations))]
    public partial class City
    {
    }
    public class CityAnnotations
    {
        [Display(Name = "Label_CityName", ResourceType = typeof(Messages))]
        [StringLength(50)]
        [Required(ErrorMessageResourceName = "Label_City_Name_Required", ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }

        [Display(Name = "Label_CountryName", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Label_Country_Required", ErrorMessageResourceType = typeof(Messages))]
        public Nullable<int> CountryId { get; set; }
    }
}
