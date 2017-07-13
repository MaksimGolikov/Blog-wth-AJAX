using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;
using Blog.Entity;


namespace Blog.Repositories
{
    public class MessageFunction
    {

        private BlogContext DBconnect;


        public MessageFunction(BlogContext newDBconnect)
        {
            DBconnect = newDBconnect;
        }

        public IEnumerable<Message> GetMessage(int idTopic)
        {
            var retrnedValue = DBconnect.Messages.Where(m=>m.IdTopic == idTopic);
            return retrnedValue;
        }
        public IEnumerable<Message> GetAllMessage()
        {
            var retrnedValue = DBconnect.Messages;
            return retrnedValue;
        }

        public void AddMessage(Message message)
        {
            DBconnect.Messages.Add(message);
            DBconnect.SaveChanges();
        }



    }
}