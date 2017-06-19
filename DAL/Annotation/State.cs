using Domain.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [MetadataType(typeof(StateAnnotations))]
    public partial  class State
    {
    }

    public class StateAnnotations
    {
        [Display(Name = "Label_StateName", ResourceType = typeof(Messages))]
        [StringLength(50)]
        [Required(ErrorMessageResourceName = "Label_State_Name_Required",
            ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }

        [Display(Name = "Label_CityName", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Label_City_Required", ErrorMessageResourceType = typeof(Messages))]
        public Nullable<int> CityId { get; set; }
    }
}
