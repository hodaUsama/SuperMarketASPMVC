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
    public class FormAccessViewModel
    {
        public FormAccess FormAccess { get; set; }
        public SelectList frms { get; set; }
        public SelectList Rols { get; set; }

    }
}