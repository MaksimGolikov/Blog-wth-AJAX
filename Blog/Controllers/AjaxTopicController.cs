using Blog.Models;
using Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blog.Controllers.Ajax
{
    public class AjaxTopicController : ApiController
    {
        private TopicService topicService;

        public AjaxTopicController()
        {
            topicService = new TopicService();
        }

                
        public Topic Get(int id)
        {
            var topic = topicService.GetTopic(id);
            return topic;
        }


       

    }
}
