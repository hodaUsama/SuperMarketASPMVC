using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using System.Web.Mvc;

namespace Design
{
    public class CityViewModel
    {
        public City objCity { get; set; }
        public SelectList Cntries { get; set; }

        
    }
}