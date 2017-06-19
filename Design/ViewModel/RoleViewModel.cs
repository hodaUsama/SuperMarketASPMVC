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
    public class RoleViewModel
    {
        public Role _Role { get; set; }
        public List<usr> Users { get; set; }

       
    }
    public class usr
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }

}