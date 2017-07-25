using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models.AuthModel;
using Blog.Models;
using System.Web.Security;
using Blog.Service;
using Blog.Models.ViewModels;

namespace Blog.Controllers
{
    public class AuthenticationController : Controller
    {

        private UserService userService;

        public AuthenticationController()
        {
            userService = new UserService();
        }


        [HttpGet]
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel() { Login = new Login(),
                                                          MasterPage = UpdateMasterPageData()
                                                        };            
            return View("LogIN", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {

            if (ModelState.IsValid)
            {
                if (!userService.ExistUser(model.LoginName))
                {
                    LoginViewModel Newmodel = new LoginViewModel() { Login = new Login(),
                                                                     MasterPage = UpdateMasterPageData()
                                                                   };   
                    return View("LogIN", Newmodel);
                }
                FormsAuthentication.SetAuthCookie(model.LoginName, true);
                return RedirectToAction("Index","Home");

            }
            LoginViewModel Newmod = new LoginViewModel() { Login = new Login(),
                                                           MasterPage = UpdateMasterPageData()
                                                          };
            return View("LogIN", Newmod);
        }


        [HttpGet]
        public ActionResult Register()
        {
            AuthenticationViewModel model = new AuthenticationViewModel()  { Registred = new Registred(),
                                                                             MasterPage = UpdateMasterPageData()
                                                                           };           
            return View("Autification", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Registred model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    AuthenticationViewModel Newmodel = new AuthenticationViewModel()  {  Registred = new Registred(),
                                                                                         MasterPage = UpdateMasterPageData()
                                                                                       }; 
                    return View("Autification", Newmodel);
                }

                if (!userService.ExistUser(model.Login))
                {
                    userService.CreateNewUser(model);
                    if (userService.ExistUser(model.Login))
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            AuthenticationViewModel Newmodels = new AuthenticationViewModel() { Registred = new Registred(),
                                                                                MasterPage = UpdateMasterPageData()
                                                                              };           
            return View("Autification", Newmodels);
        }



        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }








        private ForMasterPage UpdateMasterPageData()
        {
            var newMode = new ForMasterPage();
            var user = userService.GetUser(User.Identity.Name);

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