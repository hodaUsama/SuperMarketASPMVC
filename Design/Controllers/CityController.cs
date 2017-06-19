using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Domain;

namespace Design.Controllers
{
    public class CityController : Controller
    {
        private readonly CityService _CityService;
        private readonly CountryService _CountryService;

        public CityController() : this(new CityService(),new CountryService())
        {

        }
        public CityController(CityService objCityService, CountryService objCountryService)
        {
            _CityService = objCityService;
            _CountryService = objCountryService;

        }
        public ActionResult Index(string Keywords, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = Keywords;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CountryNameSortParm = sortOrder == "CountryName" ? "CountryName_desc" : "CountryName";

            var _Cities = _CityService.GetAll();
            switch (sortOrder)
            {
                case "name_desc":
                    _Cities = _Cities.OrderByDescending(s => s.Name);
                    break;
                case "CountryName":
                    _Cities = _Cities.OrderBy(s => s.Country.Name);
                    break;
                default:
                    _Cities = _Cities.OrderBy(s => s.Name);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                _Cities = _Cities.Where(ctry => ctry.Name.Contains(Keywords));
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(_Cities.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Delete(int id)
        {
            _CityService.Delete(new City { Id = id });
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            var model = new CityViewModel();
            model.Cntries = new SelectList(_CountryService.GetAll(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CityViewModel modl)
        {
            if (ModelState.IsValid)
            {
                _CityService.Add(modl.objCity);
                return RedirectToAction("Index");
            }
            return View("Create", modl);
        }
        public ActionResult Edit(int id)
        {
            City cty = _CityService.Get(id);
            CityViewModel model = new CityViewModel();
            model.objCity = cty;
            model.Cntries = new SelectList(_CountryService.GetAll(), "Id", "Name", cty.CountryId);           
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CityViewModel modl)
        {
            if (ModelState.IsValid)
            {
                _CityService.Update(modl.objCity);
                return RedirectToAction("Index");
            }
            return View(modl);
        }
        public ActionResult QuickSearch(string Term)
        {
            if (string.IsNullOrWhiteSpace(Term))
            {
                return Json(new String[0], JsonRequestBehavior.AllowGet);
            }
            else
            {
                IQueryable<City> ctis = _CityService.GetAll();
                return Json(ctis.Where(u => u.Name.Contains(Term)).Take(10)
              .Select(u => u.Name), JsonRequestBehavior.AllowGet);
            }
        }
    }
}