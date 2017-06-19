using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Domain;

namespace Design.Controllers
{
    public class RoleController : Controller
    {
        // GET: User
        private readonly UserServices _UserServices;
        private readonly RoleServices _RoleServices;
        public RoleController() : this(new UserServices(),new RoleServices())
        {

        }
        public RoleController(UserServices _userServices, RoleServices _roleServices)
        {
            _UserServices = _userServices;
            _RoleServices = _roleServices;
        }
        public ActionResult Index(string Keywords, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = Keywords;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DescriptionSortParm = sortOrder == "Description" ? "Description_desc" : "Description";
           
            var Roles = _RoleServices.GetAll();
            switch (sortOrder)
            {
                case "name_desc":
                    Roles = Roles.OrderByDescending(s => s.Name);
                    break;
                case "Description":
                    Roles = Roles.OrderBy(s => s.Description);
                    break;               
                default:
                    Roles = Roles.OrderBy(s => s.Name);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                Roles = Roles.Where(rol => rol.Name.Contains(Keywords));
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(Roles.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Delete(int id)
        {
            _RoleServices.Delete(new Role { Id = id });
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            RoleViewModel ObjRoleViewModel = new RoleViewModel();
            List<User> AllUsers = _UserServices.GetAll().ToList<User>();
            List<usr> Allbindes = new List<usr>();
            foreach (User us in AllUsers)
            {
                Allbindes.Add(new usr { Name = us.UserName, ID = us.Id });
            }
            ObjRoleViewModel.Users = Allbindes;
            return View(ObjRoleViewModel);
        }
        [HttpPost]
        public ActionResult Create(RoleViewModel ObjRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                List<User> lstUsr = new List<User>();
                foreach (usr u in ObjRoleViewModel.Users.Where(x => x.Selected == true))
                {
                    lstUsr.Add(new User(u.ID));
                }
                _RoleServices.Add(ObjRoleViewModel._Role, lstUsr);
                
                return RedirectToAction("Index");
            }
            return View("Create", ObjRoleViewModel);
        }
        public ActionResult Edit(int id)
        {
            Role rol = _RoleServices.Get(id);
            RoleViewModel myviewmodel = new RoleViewModel();
            myviewmodel._Role = rol;
            ICollection<User> specifiedUsers = _RoleServices.Get(id).Users.ToList();

            List<User> AllUsers = _UserServices.GetAll().ToList<User>();
            List<usr> Allbindes = new List<usr>();

            foreach (User us in AllUsers)
            {
                bool selct = false;
                if (specifiedUsers.Where(x => x.Id == us.Id).Count() > 0)
                    selct = true;
                Allbindes.Add(new usr { Name = us.UserName, ID = us.Id, Selected = selct });
            }
            myviewmodel.Users = Allbindes;
            return View(myviewmodel);
        }
      
        [HttpPost]
        public ActionResult Edit(RoleViewModel ObjRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                List<User> lstusr = new List<User>();
                foreach (usr u in ObjRoleViewModel.Users.Where(x => x.Selected == true))
                {
                    lstusr.Add(new User(u.ID));
                }
                _RoleServices.Update(ObjRoleViewModel._Role, lstusr);
                return RedirectToAction("Index");
            }
            return View(ObjRoleViewModel);
        }
        public ActionResult QuickSearch(string Term)
        {
            if (string.IsNullOrWhiteSpace(Term))
            {
                return Json(new String[0], JsonRequestBehavior.AllowGet);
            }
            else
            {
                IQueryable<Role> rols = _RoleServices.GetAll();
                return Json(rols.Where(u => u.Name.Contains(Term)).Take(10)
              .Select(u => u.Name), JsonRequestBehavior.AllowGet);
            }
        }
    }
}