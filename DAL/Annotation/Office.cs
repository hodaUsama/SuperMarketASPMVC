using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain.Resources;

namespace Domain
{
    [MetadataType(typeof(OfficeAnnotations))]
    public partial class Office
    {
    }

    public class OfficeAnnotations
    {
      

        [Display(Name = "Label_OfficeCode", ResourceType = typeof(Messages))]
        public Nullable<int> OfficeCode { get; set; }

        [Display(Name = "Label_OfficeState", ResourceType = typeof(Messages))]
        public Nullable<int> State { get; set; }

        [Display(Name = "Label_OfficeCity", ResourceType = typeof(Messages))]
        public Nullable<int> City { get; set; }

        [Display(Name = "Label_OfficeCountry", ResourceType = typeof(Messages))]
        public Nullable<int> Country { get; set; }

        [Display(Name = "Label_Officephone", ResourceType = typeof(Messages))]
        public string Phone { get; set; }

        [Display(Name = "Label_OfficePostalCode", ResourceType = typeof(Messages))]
        public string PostalCode { get; set; }

    }
}
