using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Service;
using Blog.Models;

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

            Tuple<Topic, IEnumerable<Message>, ForMasterPage> model =
                        new Tuple<Topic, IEnumerable<Message>, ForMasterPage>(topicService.GetTopic(id),
                                                                              topicService.GetMessageByTopicId(id),
                                                                              UpdateMasterPageData());
            return View("ShowSelected", model);
        }

        public ActionResult GetContext(string urlSite)
        {
            if (urlSite == null)
            {
                Tuple<Topic, ForMasterPage> model = new Tuple<Topic, ForMasterPage>(new Topic(), UpdateMasterPageData());
                return View("~/Views/Admin/CreateTopic.cshtml", model);
            }
            Tuple<Topic, ForMasterPage> modelDone = new Tuple<Topic, ForMasterPage>(new Topic() { ContextTopic = topicService.GetContextFromOtherSite(urlSite) },
                                                                                    UpdateMasterPageData());
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