using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Service;
using Blog.Models;
using Blog.ViewModels;


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
            CreateTopicViewModel model = new CreateTopicViewModel() { Topic= new Topic(),
                                                                      MasterPage = UpdateMasterPageData()
                                                                    };
           
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
            EditTopicViewModel model = new EditTopicViewModel() { Topics = adminService.GetAllTopic(),
                                                                  MasterPage = UpdateMasterPageData()
                                                                };
            
            return View("~/Views/Admin/EditTopic.cshtml", model);
        }

        [HttpPost]
        public ActionResult Edit(Topic topic)
        {
            if (topic.NameTopic == null || topic.ContextTopic == null)
            {
                return RedirectToAction("Edit");
            }
            adminService.ChangeTopic(topic);

            return RedirectToAction("AdminIndex");

        }



        [HttpGet]
        public ActionResult Delete()
        {
            DeleteViewModel model = new DeleteViewModel() { Topics = adminService.GetAllTopic(),
                                                            MasterPage = UpdateMasterPageData()
                                                           };

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
            ChangeUserRoleViewModel model = new ChangeUserRoleViewModel() { Users = adminService.GetUsers() ,
                                                                            MasterPage = UpdateMasterPageData()
                                                                            };

            return View("~/Views/Admin/ChangeUserRole.cshtml", model);
        }

        public ActionResult ChangeUserRole(User user)
        {
            adminService.ChangeUserRole(user.Role, user.Id);

            ChangeUserRoleViewModel model = new ChangeUserRoleViewModel() { Users = adminService.GetUsers(),
                                                                            MasterPage = UpdateMasterPageData()
                                                                          };
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
            newMode.Language = Session["language"].ToString();

            return newMode;
        }





    }
}