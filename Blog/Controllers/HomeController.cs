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
        private TopicService topicService;      

        public HomeController()
        {           
            topicService = new TopicService();           
        }


        public ActionResult Index()
        {            
            Tuple<IEnumerable<Topic>, ForMasterPage> model = new Tuple<IEnumerable<Topic>, ForMasterPage>(topicService.GetAllTopic(),
                                                                                                          UpdateMasterPageData()); 
            return View("Index",model);
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
            return newMode;
        }
    }
}