using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Repositories;
using Blog.Entity;
using Blog.Models;


namespace Blog.Service
{
    public class TopicService
    {

        private TopicFunction topicRepositiries;


        public TopicService ()
        {
           BlogContext DBcontext = new Entity.BlogContext();
           topicRepositiries = new TopicFunction(DBcontext);
        }


        public IEnumerable<Topic> GetAllTopic()
        {
            var topics = topicRepositiries.GetAllTopic();
            return topics;
        }

        public Topic GetTopic(int idTopic)
        {
           return topicRepositiries.GetTopicContext(idTopic);
        }


    }
}