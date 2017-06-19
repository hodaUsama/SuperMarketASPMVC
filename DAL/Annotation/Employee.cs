using Domain.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [MetadataType(typeof(EmployeeAnnotations))]
    public partial class Employee
    {
    }
    public class EmployeeAnnotations
    {
        [Display(Name = "Label_EmployNumber", ResourceType = typeof(Messages))]       
        [Required(ErrorMessageResourceName = "Label_EmployNumber_Required", ErrorMessageResourceType = typeof(Messages))]
        public Nullable<int> EmployNumber { get; set; }

        [Display(Name = "Label_FirstName_Employee", ResourceType = typeof(Messages))]
        [StringLength(50)]
        [Required(ErrorMessageResourceName = "Label_FirstName_Employee_Required", ErrorMessageResourceType = typeof(Messages))]
        public string FirstName { get; set; }

        [Display(Name = "Label_LastName_Employee", ResourceType = typeof(Messages))]
        [StringLength(50)]
        [Required(ErrorMessageResourceName = "Label_LastName_Employee_Required", ErrorMessageResourceType = typeof(Messages))]
        public string LastName { get; set; }

        [Display(Name = "Label_Extension_Employee", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Label_Extension_Employee_Required", ErrorMessageResourceType = typeof(Messages))]
        public string Extension { get; set; }

        [Display(Name = "Label_Email_Employee", ResourceType = typeof(Messages))]
        [EmailAddress]
        [Required(ErrorMessageResourceName = "Label_Email_Employee_Required", ErrorMessageResourceType = typeof(Messages))]
        public string Email { get; set; }

        [Display(Name = "Label_ReportsTo_Employee", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Label_ReportsTo_Employee_Required", ErrorMessageResourceType = typeof(Messages))]
        public Nullable<int> ReportsTo { get; set; }

        [Display(Name = "Label_JobTitle_Employee", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Label_JobTitle_Employee_Required", ErrorMessageResourceType = typeof(Messages))]
        public Nullable<int> JobTitle { get; set; }

        [Display(Name = "Label_OfficeId_Employee", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Label_OfficeId_Employee_Required", ErrorMessageResourceType = typeof(Messages))]
        public Nullable<int> OfficeId { get; set; }
    }
}
