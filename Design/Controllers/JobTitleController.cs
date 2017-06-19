using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Domain;

namespace Design.Controllers
{
    public class JobTitleController : Controller
    {

        private readonly JobTitleService _JobTitleService;
       
        public JobTitleController() : this(new JobTitleService())
        {

        }
        public JobTitleController(JobTitleService objJobTitleService)
        {
            _JobTitleService = objJobTitleService;
        }
        public ActionResult Index(string Keywords, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = Keywords;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var _Jobtitles = _JobTitleService.GetAll();
            switch (sortOrder)
            {
                case "name_desc":
                    _Jobtitles = _Jobtitles.OrderByDescending(s => s.JobTitle1);
                    break;               
                default:
                    _Jobtitles = _Jobtitles.OrderBy(s => s.JobTitle1);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                _Jobtitles = _Jobtitles.Where(jotitle => jotitle.JobTitle1.Contains(Keywords));
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(_Jobtitles.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Delete(int id)
        {
            _JobTitleService.Delete(new JobTitle { Id = id });
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Create(JobTitle jobtitl)
        {
            if (ModelState.IsValid)
            {
                _JobTitleService.Add(jobtitl);
                return RedirectToAction("Index");
            }
            return View("Create", jobtitl);
        }
        public ActionResult Edit(int id)
        {
            JobTitle titles = _JobTitleService.Get(id);
            return View(titles);
        }

        [HttpPost]
        public ActionResult Edit(JobTitle title)
        {
            if (ModelState.IsValid)
            {
                _JobTitleService.Update(title);
                return RedirectToAction("Index");
            }
            return View(title);
        }
        public ActionResult QuickSearch(string Term)
        {
            if (string.IsNullOrWhiteSpace(Term))
            {
                return Json(new String[0], JsonRequestBehavior.AllowGet);
            }
            else
            {
                IQueryable<JobTitle> ctries = _JobTitleService.GetAll();
                return Json(ctries.Where(u => u.JobTitle1.Contains(Term)).Take(10)
              .Select(u => u.JobTitle1), JsonRequestBehavior.AllowGet);
            }
        }
    }
}