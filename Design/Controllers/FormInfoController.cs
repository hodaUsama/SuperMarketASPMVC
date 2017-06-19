using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Domain;

namespace Design.Controllers
{
    public class FormInfoController : Controller
    {
        // GET: FormInfo
        private readonly FormInfoService _FormInfoService;
        public FormInfoController() : this(new FormInfoService())
        {

        }
        public FormInfoController(FormInfoService forminfoService)
        {
            _FormInfoService = forminfoService;
        }

        public ActionResult Index(string Keywords, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = Keywords;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DescriptionSortParm = sortOrder == "Description" ? "Description_desc" : "Description";

            var Forminfs = _FormInfoService.GetAll();
            switch (sortOrder)
            {
                case "name_desc":
                    Forminfs = Forminfs.OrderByDescending(s => s.Name);
                    break;
                case "Description":
                    Forminfs = Forminfs.OrderBy(s => s.Description);
                    break;
                default:
                    Forminfs = Forminfs.OrderBy(s => s.Name);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                Forminfs = Forminfs.Where(frm => frm.Name.Contains(Keywords));
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(Forminfs.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Delete(int id)
        {
            _FormInfoService.Delete(new FormInfo { Id = id });
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormInfo frm)
        {
            if (ModelState.IsValid)
            {
                _FormInfoService.Add(frm);
                return RedirectToAction("Index");
            }
            return View("Create", frm);
        }

        public ActionResult Edit(int id)
        {
            var emp = _FormInfoService.Get(id);
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(FormInfo frm)
        {
            if (ModelState.IsValid)
            {
                _FormInfoService.Update(frm);
                return RedirectToAction("Index");
            }
            return View(frm);
        }

        public ActionResult QuickSearch(string Term)
        {
            if (string.IsNullOrWhiteSpace(Term))
            {
                return Json(new String[0], JsonRequestBehavior.AllowGet);
            }
            else
            {
                IQueryable<FormInfo> frms = _FormInfoService.GetAll();
                return Json(frms.Where(u => u.Name.Contains(Term)).Take(10)
              .Select(u => u.Name), JsonRequestBehavior.AllowGet);
            }
        }
    }
}