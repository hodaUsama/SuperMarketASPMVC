using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Domain;
using Design.ViewModel;

namespace Design.Controllers
{
    public class OfficeController : Controller
    {
        
        private readonly OfficeService _OfficeService;
        private readonly CountryService _CountryService;
        private readonly CityService _CityService;
        private readonly StateService _StateService;
        public OfficeController() : this(new OfficeService(),new CountryService(),new CityService(),new StateService())
        {

        }
        public OfficeController(OfficeService OfficeService,CountryService countyser,CityService ctyservc,StateService staserv)
        {
            _OfficeService = OfficeService;
            _CountryService = countyser;
            _CityService = ctyservc;
            _StateService = staserv;
        }

        public ActionResult Index(string Keywords, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = Keywords;

            ViewBag.OfficeCodeSortParm = String.IsNullOrEmpty(sortOrder) ? "OfficeCode_desc" : "";
            ViewBag.StateSortParm = sortOrder == "State" ? "State_desc" : "State";
            ViewBag.CitySortParm = sortOrder == "City" ? "City_desc" : "City";
            ViewBag.CountrySortParm = sortOrder == "Country" ? "Country_desc" : "Country";

            var offices = _OfficeService.GetAll();
            switch (sortOrder)
            {
                case "OfficeCode_desc":
                    offices = offices.OrderByDescending(s => s.OfficeCode);
                    break;
                case "State":
                    offices = offices.OrderBy(s => s.State);
                    break;
                case "City":
                    offices = offices.OrderBy(s => s.City);
                    break;
                case "Country":
                    offices = offices.OrderBy(s => s.Country);
                    break;
                default:
                    offices = offices.OrderBy(s => s.OfficeCode);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                offices = offices.Where(ofc => ofc.Country1.Name.Contains(Keywords));
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(offices.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Delete(int id)
        {
            _OfficeService.Delete(new Office { Id = id });
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            var model = new OfficeViewModel();
            model.Cntries = new SelectList(_CountryService.GetAll(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(OfficeViewModel objOfficeViewModel)
        {
            if (ModelState.IsValid)
            {
                _OfficeService.Add(objOfficeViewModel.objoffice);
                return RedirectToAction("Index");
            }
            return View("Create", objOfficeViewModel);
        }

        public ActionResult Edit(int id)
        {
            Office ofc = _OfficeService.Get(id);
            OfficeViewModel model = new OfficeViewModel();
            model.objoffice = ofc;
            model.Cntries = new SelectList(_CountryService.GetAll(), "Id", "Name", ofc.Country);
            model.Cties = new SelectList(_CityService.GetCitiesByCountry(Convert.ToInt32(ofc.Country))
                .Select(x => new { x.Id, x.Name }), "Id", "Name", ofc.City);
            model.states = new SelectList(_CityService.Getstatebycity(Convert.ToInt32(ofc.City))
             .Select(x => new { x.Id, x.Name }), "Id", "Name", ofc.State);


            model.CntrySelect = Convert.ToInt32(ofc.Country);
            model.CitySelect = Convert.ToInt32(ofc.City);
            model.StateSelect = Convert.ToInt32(ofc.State);
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(OfficeViewModel objOfficeViewModel)
        {
            if (ModelState.IsValid)
            {
                _OfficeService.Update(objOfficeViewModel.objoffice);
                return RedirectToAction("Index");
            }
            return View(objOfficeViewModel);
        }

        public ActionResult QuickSearch(string Term)
        {
            if (string.IsNullOrWhiteSpace(Term))
            {
                return Json(new String[0], JsonRequestBehavior.AllowGet);
            }
            else
            {
                IQueryable<Office> offcs = _OfficeService.GetAll();
                return Json(offcs.Where(u => u.Country1.Name.Contains(Term)).Take(10)
              .Select(u => u.Country1.Name), JsonRequestBehavior.AllowGet);
            }
        }
    }
}