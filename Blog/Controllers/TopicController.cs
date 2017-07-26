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
    public class TopicController : Controller
    {

        private TopicService topicService;

        public TopicController()
        {
            topicService = new TopicService();
        }


        public ActionResult ShowSelected(int id)
        {
            ShowSelectedViewModel model = new ShowSelectedViewModel() { Comments = topicService.GetMessageByTopicId(id),
                                                                        MasterPage = UpdateMasterPageData(),
                                                                        Topic = topicService.GetTopic(id)
                                                                    };           
            return View("ShowSelected", model);
        }

        public ActionResult GetContext(string urlSite)
        {
            if (urlSite == null)
            {
                CreateTopicViewModel model = new CreateTopicViewModel() { Topic= new Topic(),
                                                                          MasterPage = UpdateMasterPageData()
                                                                        };                
                return View("~/Views/Admin/CreateTopic.cshtml", model);
            }
            CreateTopicViewModel modelDone = new CreateTopicViewModel(){ Topic = new Topic() { ContextTopic = topicService.GetContextFromOtherSite(urlSite) },
                                                                         MasterPage = UpdateMasterPageData()
                                                                       };      

            return View("~/Views/Admin/CreateTopic.cshtml", modelDone);

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