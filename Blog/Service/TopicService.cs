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

        private TopicFunction topicRepos;


        public TopicService ()
        {
           BlogContext DBcontext = new Entity.BlogContext();
           topicRepos = new TopicFunction(DBcontext);
        }


        public IEnumerable<Topic> GetAllTopic()
        {
            var topics = topicRepos.GetAllTopic();
            return topics;
        }

        public Topic GetTopic(int idTopic)
        {
           return topicRepos.GetTopicContext(idTopic);
        }


    }
}