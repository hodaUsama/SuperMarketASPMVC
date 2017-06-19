using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using System.Web.Mvc;

namespace Design
{
    public class EmployeeViewModel
    {
        public Employee objemplyee { get; set; }
        public SelectList offices { get; set; }
        public int officeSelect { get; set; }
        public SelectList employees { get; set; }
        public int empselect { get; set; }
        public SelectList reportsTo { get; set; }
        public SelectList jobtitles { get; set; }
        public int jobtitleSelect { get; set; }

    }
}