using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Domain.Resources;

namespace Design
{
    public class RoleUserViewModel
    {
        public User _User { get; set; }

        [Display(Name = "lst_AllRoles", ResourceType = typeof(Messages))]
        public List<Role> NotSpecifiedUser { get; set; }
        [Display(Name = "lst_UserRole", ResourceType = typeof(Messages))]
        public List<Role> RolesForSpecifiedUser { get; set; }

       
        public int[] AllSelected { get; set; }
        public int[] SpecifiedSelected { get; set; }
       

    }

   
}