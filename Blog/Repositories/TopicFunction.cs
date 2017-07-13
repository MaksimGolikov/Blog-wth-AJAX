using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Blog.Models;
using Blog.Entity;

namespace Blog.Repositories
{
    public class TopicFunction
    {
        private BlogContext DBconnect;

        public TopicFunction(BlogContext newDBconnect)
        {
            DBconnect = newDBconnect;
        }



        public IEnumerable<Topic> GetAllTopic()
        {
            return DBconnect.Topic;
        }
        public void AddNewTopic(Topic newTopic)
        {           
            DBconnect.Topic.Add(newTopic);
            DBconnect.SaveChanges();
        }

        public Topic GetTopicContext(int idTopic)
        {
            var context = DBconnect.Topic.Find(idTopic);
            return context;
        }

        public void ChangeTopic(Topic topic)
        {    
            var local = DBconnect.Topic.Where(t=>t.Id == topic.Id).FirstOrDefault();
            local.NameTopic = topic.NameTopic;
            local.ContextTopic = topic.ContextTopic;

            DBconnect.Entry(local).State = EntityState.Modified;
            DBconnect.SaveChanges();
        }

        public void DeleteTopic(int idTopic)
        {
            var t = DBconnect.Topic.Find(idTopic);
            DBconnect.Topic.Remove(t);
            DBconnect.SaveChanges();
        }
        

        
    }
}