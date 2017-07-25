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
using Blog.Models.ViewModels;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {      
        private TopicService topicService;
            
        public HomeController()
        {           
            topicService = new TopicService();
                                
        }



        public ActionResult Index()
        {
            if (Session["language"] == null)
            {
                Session["language"] = "en";                
            }

            FirstPageViewModel model = new FirstPageViewModel() { Topics = topicService.GetAllTopic(),
                                                                  MasterPage = UpdateMasterPageData()
                                                                };
            
            return View("Index",model);
        }

        public ActionResult Language(string lang)
        {
            var r = Request.Url.ToString();
            Session["language"] = lang;

            return Redirect(Request.UrlReferrer.ToString());           
         }
       

        public ActionResult Me()
        {
            return View("AboutMe", UpdateMasterPageData());
        }
        public ActionResult Gold()
        {
            GoldViewModel model = new GoldViewModel() { UserName = User.Identity.Name,
                                                        MasterPage = UpdateMasterPageData()
                                                      };
           
            return View("Gold",model);
        }                


        private ForMasterPage UpdateMasterPageData()
        {
            var newMode = new ForMasterPage();
            var user = topicService.GetUser(User.Identity.Name);

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