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
        private BlogContext dbConnect;

        public TopicFunction(BlogContext newDbconnect)
        {
            dbConnect = newDbconnect;
        }



        public IEnumerable<Topic> GetAllTopic()
        {
            return dbConnect.Topic;
        }
        public void AddNewTopic(Topic newTopic)
        {
            dbConnect.Topic.Add(newTopic);
            dbConnect.SaveChanges();
        }

        public Topic GetTopicContext(int idTopic)
        {
            var context = dbConnect.Topic.Find(idTopic);
            return context;
        }

        public void ChangeTopic(Topic topic)
        {    
            var local = dbConnect.Topic.Where(t=>t.Id == topic.Id).FirstOrDefault();
            local.NameTopic = topic.NameTopic;
            local.ContextTopic = topic.ContextTopic;

            dbConnect.Entry(local).State = EntityState.Modified;
            dbConnect.SaveChanges();
        }

        public void DeleteTopic(int idTopic)
        {
            var t = dbConnect.Topic.Find(idTopic);
            dbConnect.Topic.Remove(t);
            dbConnect.SaveChanges();
        }

        public bool TopicExist(Topic topic)
        {
            var returned = true;
            var fromDb = dbConnect.Topic.Where(t=> t.Id == topic.Id).FirstOrDefault();;
            if(fromDb == null)
            {
                returned = false;
            }

            return returned;
        }
        
    }
}