using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Service;
using Blog.Models;
using System.Web.Security;
using Blog.Models.AuthModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using HtmlAgilityPack;


namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private AdminSevrice adminService;
        private MessageService messageService;
        private UserService userService;
        private TopicService topicService;      

        public HomeController()
        {
            adminService = new AdminSevrice();
            messageService = new MessageService();
            userService = new UserService();
            topicService = new TopicService();           
        }


        public ActionResult Index()
        {            
            Tuple<IEnumerable<Topic>, ForMasterPage> model = new Tuple<IEnumerable<Topic>, ForMasterPage>(topicService.GetAllTopic(),
                                                                                                          UpdateMasterPageData()); 
            return View("Index",model);
        }

        public ActionResult ShowSelected(int id)
        {
            
            Tuple<Topic, IEnumerable<Message>,ForMasterPage> model =
                        new Tuple<Topic,IEnumerable<Message>,ForMasterPage>(topicService.GetTopic(id),
                                                                            messageService.GetMessageByTopicId(id),
                                                                            UpdateMasterPageData());
            return View("ShowSelected",model);
        }

        public ActionResult Me()
        {
            return View("AboutMe", UpdateMasterPageData());
        }
        public ActionResult Gold()
        {
            Tuple<string, ForMasterPage> model =
                new Tuple<string, ForMasterPage>(User.Identity.Name,
                                                 UpdateMasterPageData());
            return View("Gold",model);
        }

        public ActionResult ChangeUser(User changedUser)
        {
            userService.ChangeUserInformation(changedUser);
            return RedirectToAction("Index");
        }


         #region Auntification


        [HttpGet]
        public ActionResult Login()
        {
            Tuple<Login, ForMasterPage> model = new Tuple<Login, ForMasterPage>(new Login(), UpdateMasterPageData());
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
                    Tuple<Login, ForMasterPage> Newmodel = new Tuple<Login, ForMasterPage>(new Login(), UpdateMasterPageData());
                    return View("LogIN", Newmodel);
                }
                FormsAuthentication.SetAuthCookie(model.LoginName, true);
                return RedirectToAction("Index");     
               
            }
            Tuple<Login, ForMasterPage> Newmod = new Tuple<Login, ForMasterPage>(new Login(), UpdateMasterPageData());
            return View("LogIN", Newmod); 
        }

        
        [HttpGet]
        public ActionResult Register()
        {
            Tuple<Registred, ForMasterPage> model = new Tuple<Registred, ForMasterPage>(new Registred(), UpdateMasterPageData());
            return View("Autification", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Registred model)
        {
            if (ModelState.IsValid)
            {
                if(model.Password != model.ConfirmPassword)
                {
                    Tuple<Registred, ForMasterPage> Newmodel = new Tuple<Registred, ForMasterPage>(new Registred(), UpdateMasterPageData());
                    return View("Autification", Newmodel);    
                }

                if(!userService.ExistUser(model.Login))
                {
                    userService.CreateNewUser(model);
                    if (userService.ExistUser(model.Login))
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index");
                    }
                }
            }
            Tuple<Registred, ForMasterPage> Newmodels = new Tuple<Registred, ForMasterPage>(new Registred(), UpdateMasterPageData());
            return View("Autification", Newmodels);           
        }

               
        
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();                   
            return RedirectToAction("Index");
        }

        public ActionResult UserProfile()
        {
            Tuple<User, ForMasterPage> model = new Tuple<User, ForMasterPage>(userService.GetUser(User.Identity.Name), UpdateMasterPageData());
            return View("Profile", model);
        }
        public ActionResult ChangeProfile(User us)
        {
            userService.ChangeUserInformation(us);

            return RedirectToAction("Index");
        }


        #endregion


          #region Admin function


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
              if(id.NameTopic == null || id.ContextTopic == null)
              {
                   return RedirectToAction("AdminIndex");
              }

              adminService.AddTopic(id); 
              return RedirectToAction("AdminIndex");
          }
        

          public ActionResult Edit()
          {
              Tuple<IEnumerable<Topic>, ForMasterPage> model = 
                        new Tuple<IEnumerable<Topic>, ForMasterPage>(topicService.GetAllTopic(),                                                                           
                                                                            UpdateMasterPageData());
              return View("~/Views/Admin/EditTopic.cshtml", model);
          }

          [HttpPost]
          public ActionResult Edit(Topic t)
          {
              if(t.NameTopic == null || t.ContextTopic == null)
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
                  new Tuple<IEnumerable<Topic>,ForMasterPage> (topicService.GetAllTopic(),
                                                                UpdateMasterPageData() );
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
                                     new Tuple<IEnumerable<User>, ForMasterPage>(userService.GetUsers(),
                                                                                         UpdateMasterPageData());
              return View("~/Views/Admin/ChangeUserRole.cshtml", model);
          }
         
          public ActionResult ChangeUserRole(User user)
          {              
              adminService.ChangeUserRole(user.Role,user.Id);

              Tuple<IEnumerable<User>, ForMasterPage> model =
                                     new Tuple<IEnumerable<User>, ForMasterPage>(userService.GetUsers(),
                                                                                         UpdateMasterPageData());
              return View("~/Views/Admin/ChangeUserRole.cshtml", model);
          }


          public ActionResult GetContext(string urlSite)
          {
              if (urlSite == null)
              {
                  Tuple<Topic, ForMasterPage> model = new Tuple<Topic, ForMasterPage>(new Topic(), UpdateMasterPageData());
                  return View("~/Views/Admin/CreateTopic.cshtml", model); 
              }
              Tuple<Topic, ForMasterPage> modelDone = new Tuple<Topic, ForMasterPage>(new Topic() { ContextTopic = adminService.GetContextFromOtherSite(urlSite) },
                                                                                      UpdateMasterPageData());
              return View("~/Views/Admin/CreateTopic.cshtml", modelDone);
              
          }
#endregion


        private ForMasterPage UpdateMasterPageData()
        {
            var newMode = new ForMasterPage();

            if (!User.Identity.IsAuthenticated)
            {
                newMode.UserAuthorisaed = false;
                newMode.UserName = "Guest";
                newMode.UserRole = "guest";
            }
            if (User.Identity.IsAuthenticated)
            {
                newMode.UserAuthorisaed = true;
                var user = userService.GetUser(User.Identity.Name);
                newMode.UserName = user.FirstName;
                newMode.UserRole = user.Role;
            }
            return newMode;
        }
    }
}