using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using System.Web.Mvc;

namespace Design.ViewModel
{
    public class OfficeViewModel
    {
        public Office objoffice { get; set; }
        public SelectList Cntries { get; set; }
        public int CntrySelect { get; set; }
        public SelectList Cties { get; set; }
        public int CitySelect { get; set; }
        public SelectList states { get; set; }
        public int StateSelect { get; set; }
    }
}