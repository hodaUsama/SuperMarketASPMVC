using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Design.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private readonly UserServices _UserServices;
        private readonly RoleServices _RoleServices;
        public UserController() : this(new UserServices(),new RoleServices())
        {

        }
        public UserController(UserServices _userServices, RoleServices _roleServices)
        {
            _UserServices = _userServices;
            _RoleServices = _roleServices;
        }        
        public ActionResult Index(string Keywords,string sortOrder,  int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Firstname_desc" : "";
            ViewBag.SecondSortParm = sortOrder == "SecondName" ? "SecondName_desc" : "SecondName";
            ViewBag.UserSortParm = sortOrder == "UserName" ? "UserName_desc" : "UserName";
            ViewBag.DateSortParm = sortOrder == "BirthDate" ? "BirthDate_desc" : "BirthDate";
            var users = _UserServices.GetAll();
            ViewBag.CurrentFilter = Keywords;
            switch (sortOrder)
            {
                case "Firstname_desc":
                    users = users.OrderByDescending(s => s.FirstName);
                    break;
                case "BirthDate":
                    users = users.OrderBy(s => s.DateOfBirth);
                    break;
                case "BirthDate_desc":
                    users = users.OrderByDescending(s => s.DateOfBirth);
                    break;
                case "SecondName":
                    users = users.OrderBy(s => s.SecondName);
                    break;
                case "SecondName_desc":
                    users = users.OrderByDescending(s => s.SecondName);
                    break;
                case "UserName":
                    users = users.OrderBy(s => s.UserName);
                    break;
                case "UserName_desc":
                    users = users.OrderByDescending(s => s.UserName);
                    break;
                default:
                    users = users.OrderBy(s => s.FirstName);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                users = users.Where(usr => usr.FirstName.Contains(Keywords));
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View (users.ToPagedList(pageNumber, pageSize));
           
        }
        public ActionResult Delete(int id)
        {
            _UserServices.Delete(new User { Id= id });
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            RoleUserViewModel ObjRoleUserViewModel = new RoleUserViewModel();
            List<Role> AllRole = _RoleServices.GetAll().ToList<Role>();
            ObjRoleUserViewModel.NotSpecifiedUser = AllRole;
            ObjRoleUserViewModel.RolesForSpecifiedUser = new List<Role>(); 
            return View(ObjRoleUserViewModel);
        }
        [HttpPost]
        public ActionResult Create(RoleUserViewModel ObjRoleUserViewModel)
        {
            if (ModelState.IsValid)
            {
                //for role
                List<Role> lstRol = new List<Role>();
                foreach (int x in ObjRoleUserViewModel.SpecifiedSelected)
                {
                    lstRol.Add(new Role(x));
                }               
                _UserServices.Add(ObjRoleUserViewModel._User, lstRol);
                return RedirectToAction("Index");
            }
            return View("Create", ObjRoleUserViewModel);
        }       
        public ActionResult Edit(int id)
        {
            User usr = _UserServices.Get(id);
            RoleUserViewModel myviewmodel = new RoleUserViewModel();
            myviewmodel._User = usr;
            myviewmodel.NotSpecifiedUser = GetNotSelectedRole(id);
            myviewmodel.RolesForSpecifiedUser = usr.Roles.ToList<Role>();
            return View(myviewmodel);
        }
        public List<Role> GetNotSelectedRole(int userid)
        {
            ICollection<Role> setToRemove = _UserServices.Get(userid).Roles.ToList();
            List<Role> AllRole = _RoleServices.GetAll().ToList<Role>();
            foreach (Role r in setToRemove)
            {
                AllRole.RemoveAll(m => m.Id == r.Id);
            }            
            return AllRole;
        }
        [HttpPost]
        public ActionResult Edit(RoleUserViewModel ObjRoleUserViewModel)
        {
            if (ModelState.IsValid)
            {
                _UserServices.Update(ObjRoleUserViewModel._User, ObjRoleUserViewModel.SpecifiedSelected);
                return RedirectToAction("Index");
            }
            return View(ObjRoleUserViewModel);
        }
        public ActionResult QuickSearch(string Term)
        {
            if (string.IsNullOrWhiteSpace(Term))
            {
                return Json(new String[0], JsonRequestBehavior.AllowGet);
            }
            else
            {
                IQueryable<User> usrs = _UserServices.GetAll();
                return Json(usrs.Where(u => u.FirstName.Contains(Term)).Take(10)
              .Select(u => u.FirstName), JsonRequestBehavior.AllowGet);
            }
        }
    }
}