using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using PagedList;

namespace Design.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _EmployeeService;
        private readonly JobTitleService _JobTitleService;
        private readonly OfficeService _OfficeService;

        public EmployeeController() : this(new JobTitleService(),new OfficeService(),new EmployeeService())
        {

        }
        public EmployeeController(JobTitleService objJobTitleService, OfficeService objOfficeService, EmployeeService objEmployeeService )
        {
            _JobTitleService = objJobTitleService;
            _OfficeService = objOfficeService;
            _EmployeeService = objEmployeeService;

        }
        public ActionResult Index(string Keywords, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = Keywords;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "employeenumber_desc" : "";
            ViewBag.NumberSortParm = sortOrder == "Firstname" ? "Firstname_desc" : "Firstname";

            var _employees = _EmployeeService.GetAll();
            switch (sortOrder)
            {
                case "employeenumber_desc":
                    _employees = _employees.OrderByDescending(s => s.EmployNumber);
                    break;
                case "Firstname":
                    _employees = _employees.OrderBy(s => s.FirstName);
                    break;
                default:
                    _employees = _employees.OrderBy(s => s.EmployNumber);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                _employees = _employees.Where(ctry => ctry.FirstName.Contains(Keywords));
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(_employees.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Delete(int id)
        {
            _EmployeeService.Delete(new Employee { Id = id });
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            var model = new EmployeeViewModel();
            model.offices = new SelectList(_OfficeService.GetAll(), "Id", "OfficeCode");
            model.jobtitles = new SelectList(_JobTitleService.GetAll(), "Id", "JobTitle1");
            model.employees = new SelectList(_EmployeeService.GetAll(), "Id", "Firstname");
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel modl)
        {
            if (ModelState.IsValid)
            {
                _EmployeeService.Add(modl.objemplyee);
                return RedirectToAction("Index");
            }
            return View("Create", modl);
        }
        public ActionResult Edit(int id)
        {
            Employee emp = _EmployeeService.Get(id);
            EmployeeViewModel model = new EmployeeViewModel();
            model.objemplyee = emp;
            model.offices = new SelectList(_OfficeService.GetAll(), "Id", "OfficeCode");
            model.jobtitles = new SelectList(_JobTitleService.GetAll(), "Id", "JobTitle1");
            model.employees = new SelectList(_EmployeeService.GetAll(), "Id", "Firstname");
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeViewModel modl)
        {
            if (ModelState.IsValid)
            {
                _EmployeeService.Update(modl.objemplyee);
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
                IQueryable<Employee> ctis = _EmployeeService.GetAll();
                return Json(ctis.Where(u => u.FirstName.Contains(Term)).Take(10)
              .Select(u => u.FirstName), JsonRequestBehavior.AllowGet);
            }
        }
    }
}