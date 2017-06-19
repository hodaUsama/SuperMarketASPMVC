using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Domain;

namespace Design.Controllers
{
    public class FormAccessController : Controller
    {
        
        private readonly FormAccessService _FormAccessService;
        private readonly RoleServices _RoleServices;
        private readonly FormInfoService _FormInfoService;
        public FormAccessController() : this(new FormAccessService(),new RoleServices(),new FormInfoService())
        {

        }
        public FormAccessController(FormAccessService FormAccessService,RoleServices RoleServices, 
            FormInfoService FormInfoService)
        {
            _FormAccessService = FormAccessService;
            _RoleServices = RoleServices;
            _FormInfoService = FormInfoService;
        }
        public ActionResult Index(string Keywords, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = Keywords;

            ViewBag.FormNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Formname_desc" : "";
            ViewBag.RoleNameSortParm = sortOrder == "RoleName" ? "RoleName_desc" : "RoleName";

            var _FormAccess = _FormAccessService.GetAll();
            switch (sortOrder)
            {
                case "Formname_desc":
                    _FormAccess = _FormAccess.OrderByDescending(s => s.FormInfo.Name);
                    break;
                case "RoleName":
                    _FormAccess = _FormAccess.OrderBy(s => s.Role.Name);
                    break;
                default:
                    _FormAccess = _FormAccess.OrderBy(s => s.FormInfo.Name);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                _FormAccess = _FormAccess.Where(frm => frm.FormInfo.Name.Contains(Keywords));
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(_FormAccess.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Delete(int id)
        {
            _FormAccessService.Delete(new FormAccess { Id = id });
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            var model = new FormAccessViewModel();
            model.frms = new SelectList(_FormInfoService.GetAll(), "Id", "Name");
            model.Rols = new SelectList(_RoleServices.GetAll(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(FormAccessViewModel _FormAccessViewModel)
        {
            if (ModelState.IsValid)
            {
                _FormAccessService.Add(_FormAccessViewModel.FormAccess);
                return RedirectToAction("Index");
            }
            return View("Create", _FormAccessViewModel);
        }
        public ActionResult Edit(int id)
        {
            FormAccess Frmacs = _FormAccessService.Get(id);
            FormAccessViewModel model = new FormAccessViewModel();
            model.FormAccess = Frmacs;
            model.frms = new SelectList(_FormInfoService.GetAll(), "Id", "Name", Frmacs.FormIdFK);
            model.Rols = new SelectList(_RoleServices.GetAll(), "Id", "Name", Frmacs.RoleidFK);
           
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(FormAccessViewModel objFormAccessViewModel)
        {
            if (ModelState.IsValid)
            {
                _FormAccessService.Update(objFormAccessViewModel.FormAccess);
                return RedirectToAction("Index");
            }
            return View(objFormAccessViewModel);
        }
        public ActionResult QuickSearch(string Term)
        {
            if (string.IsNullOrWhiteSpace(Term))
            {
                return Json(new String[0], JsonRequestBehavior.AllowGet);
            }
            else
            {
                IQueryable<FormAccess> frms = _FormAccessService.GetAll();
                return Json(frms.Where(u => u.FormInfo.Name.Contains(Term)).Take(10)
              .Select(u => u.FormInfo.Name), JsonRequestBehavior.AllowGet);
            }
        }
    }
}