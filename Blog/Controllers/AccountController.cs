using Blog.Models;
using Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
       private UserService userService;

       public AccountController()
       {
           userService = new UserService();
       }


        public ActionResult UserProfile()
        {
            Tuple<User, ForMasterPage> model = new Tuple<User, ForMasterPage>(userService.GetUser(User.Identity.Name), UpdateMasterPageData());
            return View("Profile", model);
        }
        public ActionResult ChangeProfile(User us)
        {
            FormsAuthentication.SignOut();
            userService.ChangeUserInformation(us);

            if (userService.ExistUser(us.Login))
            {
                FormsAuthentication.SetAuthCookie(us.Login, true);
                return RedirectToAction("Index","Home");
            }

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
            return newMode;
        }



    }
}