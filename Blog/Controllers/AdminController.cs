using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Service;
using Blog.Models;

namespace Blog.Controllers
{
    public class AdminController : Controller
    {
        private AdminSevrice adminService;
        

        public AdminController()
        {
            adminService = new AdminSevrice();
        }


        public ActionResult AdminIndex()
        {
            return View("~/Views/Admin/Index.cshtml", UpdateMasterPageData());
        }


        [HttpGet]
        public ActionResult Create()
        {
            Tuple<Topic, ForMasterPage> model = new Tuple<Topic, ForMasterPage>(new Topic(), UpdateMasterPageData());
            return View("~/Views/Admin/CreateTopic.cshtml", model);
        }
        [HttpPost]
        public ActionResult Create(Topic id)
        {
            if (id.NameTopic == null || id.ContextTopic == null)
            {
                return RedirectToAction("AdminIndex");
            }

            adminService.AddTopic(id);
            return RedirectToAction("AdminIndex");
        }


        public ActionResult Edit()
        {
            Tuple<IEnumerable<Topic>, ForMasterPage> model =
                      new Tuple<IEnumerable<Topic>, ForMasterPage>(adminService.GetAllTopic(),
                                                                          UpdateMasterPageData());
            return View("~/Views/Admin/EditTopic.cshtml", model);
        }

        [HttpPost]
        public ActionResult Edit(Topic t)
        {
            if (t.NameTopic == null || t.ContextTopic == null)
            {
                return RedirectToAction("Edit");
            }
            adminService.ChangeTopic(t);

            return RedirectToAction("AdminIndex");

        }



        [HttpGet]
        public ActionResult Delete()
        {
            Tuple<IEnumerable<Topic>, ForMasterPage> model =
                new Tuple<IEnumerable<Topic>, ForMasterPage>(adminService.GetAllTopic(),
                                                              UpdateMasterPageData());
            return View("~/Views/Admin/DeleteTopic.cshtml", model);
        }

        public ActionResult Del(int id)
        {
            adminService.DeleteTopic(id);
            return RedirectToAction("AdminIndex");
        }

        [HttpGet]
        public ActionResult ChangeRole()
        {
            Tuple<IEnumerable<User>, ForMasterPage> model =
                                   new Tuple<IEnumerable<User>, ForMasterPage>(adminService.GetUsers(),
                                                                                       UpdateMasterPageData());
            return View("~/Views/Admin/ChangeUserRole.cshtml", model);
        }

        public ActionResult ChangeUserRole(User user)
        {
            adminService.ChangeUserRole(user.Role, user.Id);

            Tuple<IEnumerable<User>, ForMasterPage> model =
                                   new Tuple<IEnumerable<User>, ForMasterPage>(adminService.GetUsers(),
                                                                                       UpdateMasterPageData());
            return View("~/Views/Admin/ChangeUserRole.cshtml", model);
        }


       

        private ForMasterPage UpdateMasterPageData()
        {
            var newMode = new ForMasterPage();
            var user = adminService.GetUser(User.Identity.Name);

            if (!User.Identity.IsAuthenticated || user == null)
            {
                newMode.UserAuthorisaed = false;
                newMode.UserName = "Guest";
                newMode.UserRole = "guest";
            }
            if (User.Identity.IsAuthenticated && user != null)
            {
                newMode.UserAuthorisaed = true;
                newMode.UserName = user.FirstName;
                newMode.UserRole = user.Role;
            }
            return newMode;
        }





    }
}