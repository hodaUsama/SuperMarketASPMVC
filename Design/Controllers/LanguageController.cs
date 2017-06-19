using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Design.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Change(string LanguageAbrrevieation)
        {
            if (LanguageAbrrevieation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbrrevieation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbrrevieation);
            }
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbrrevieation;
            Response.Cookies.Add(cookie);
            var referer = Request.ServerVariables["HTTP_REFERER"];
            return Redirect(referer);
        }


    }
}