using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using System.Web.Mvc;

namespace Design
{
    public class StateViewModel
    {
        public State objState { get; set; }      
        public SelectList Cntries { get; set; }     
        public int CntrySelect { get; set; }
        public SelectList Cties { get; set; }
    }
}