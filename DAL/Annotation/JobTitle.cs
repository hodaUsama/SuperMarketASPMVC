using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain.Resources;

namespace Domain
{
    [MetadataType(typeof(JobTitleAnnotations))]
    public partial class JobTitle
    {
    }

    public class JobTitleAnnotations
    {
        [Display(Name = "Label_JobTitleName", ResourceType = typeof(Messages))]
        public string JobTitle1 { get; set; }

        [Display(Name = "Label_JobTitleDescription", ResourceType = typeof(Messages))]
        public string JobDescription { get; set; }

    }
}
