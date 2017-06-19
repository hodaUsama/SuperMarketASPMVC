using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Domain;

namespace Design.Controllers
{
    public class CountryController : Controller
    {

        private readonly CountryService _CountryService;
       
        public CountryController() : this(new CountryService())
        {

        }
        public CountryController(CountryService objCountryService)
        {
            _CountryService = objCountryService;
        }
        public ActionResult Index(string Keywords, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = Keywords;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var _Countries = _CountryService.GetAll();
            switch (sortOrder)
            {
                case "name_desc":
                    _Countries = _Countries.OrderByDescending(s => s.Name);
                    break;               
                default:
                    _Countries = _Countries.OrderBy(s => s.Name);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                _Countries = _Countries.Where(ctry => ctry.Name.Contains(Keywords));
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(_Countries.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Delete(int id)
        {
            _CountryService.Delete(new Country { Id = id });
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Create(Country ctry)
        {
            if (ModelState.IsValid)
            {
                _CountryService.Add(ctry);
                return RedirectToAction("Index");
            }
            return View("Create", ctry);
        }
        public ActionResult Edit(int id)
        {
            Country ctries = _CountryService.Get(id);
            return View(ctries);
        }

        [HttpPost]
        public ActionResult Edit(Country ctry)
        {
            if (ModelState.IsValid)
            {
                _CountryService.Update(ctry);
                return RedirectToAction("Index");
            }
            return View(ctry);
        }
        public ActionResult QuickSearch(string Term)
        {
            if (string.IsNullOrWhiteSpace(Term))
            {
                return Json(new String[0], JsonRequestBehavior.AllowGet);
            }
            else
            {
                IQueryable<Country> ctries = _CountryService.GetAll();
                return Json(ctries.Where(u => u.Name.Contains(Term)).Take(10)
              .Select(u => u.Name), JsonRequestBehavior.AllowGet);
            }
        }
    }
}