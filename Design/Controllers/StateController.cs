using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Domain;

namespace Design.Controllers
{
    public class StateController : Controller
    {
        private readonly StateService _StateService;
        private readonly CityService _CityService;
        private readonly CountryService _CountryService;
        public StateController() : this(new StateService() ,new CityService(),new CountryService())
        {

        }
        public StateController(StateService objStateService, CityService objCityService, CountryService objCountryService)
        {
            _StateService = objStateService;
            _CityService = objCityService;
            _CountryService = objCountryService;

        }
        public ActionResult Index(string Keywords, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = Keywords;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CountryNameSortParm = sortOrder == "CountryName" ? "CountryName_desc" : "CountryName";
            ViewBag.CityNameSortParm = sortOrder == "CityName" ? "CityName_desc" : "CityName";

            var _States = _StateService.GetAll();
            switch (sortOrder)
            {
                case "name_desc":
                    _States = _States.OrderByDescending(s => s.Name);
                    break;
                case "CountryName":
                    _States = _States.OrderBy(s => s.City.Name);
                    break;
                case "CityName":
                    _States = _States.OrderBy(s => s.City.CountryId);
                    break;
                default:
                    _States = _States.OrderBy(s => s.Name);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                _States = _States.Where(ctry => ctry.Name.Contains(Keywords));
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(_States.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Delete(int id)
        {
            _StateService.Delete(new State { Id = id });
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            var model = new StateViewModel();          
            model.Cntries = new SelectList(_CountryService.GetAll(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public ActionResult CityByCountry(int CountryId)
        {
            // Filter the cities by country.
            var states = _CityService.GetCitiesByCountry(CountryId).Select(x=>new { x.Id,x.Name});
            return Json(states);
        }

        //used in office controller
        [HttpPost]
        public ActionResult StateByCity(int CityId)
        {
            // Filter the cities by country.
            var states = _CityService.Getstatebycity(CityId).Select(x => new { x.Id, x.Name });
            return Json(states);
        }

        [HttpPost]
        public ActionResult Create(StateViewModel modl)
        {
            if (ModelState.IsValid)
            {
                _StateService.Add(modl.objState);
                return RedirectToAction("Index");
            }
            return View("Create", modl);
        }
        public ActionResult Edit(int id)
        {
            State stat = _StateService.Get(id);
            StateViewModel model = new StateViewModel();
            model.objState = stat;
            model.Cntries = new SelectList(_CountryService.GetAll(), "Id", "Name", stat.City.CountryId);
            model.Cties = new SelectList(_CityService.GetCitiesByCountry(Convert.ToInt32( stat.City.CountryId))
                .Select(x => new { x.Id, x.Name }), "Id", "Name", stat.CityId);
            model.CntrySelect = Convert.ToInt32(stat.City.CountryId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(StateViewModel modl)
        {
            if (ModelState.IsValid)
            {
                _StateService.Update(modl.objState);
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
                IQueryable<State> stat = _StateService.GetAll();
                return Json(stat.Where(u => u.Name.Contains(Term)).Take(10)
              .Select(u => u.Name), JsonRequestBehavior.AllowGet);
            }
        }
    }
}