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

        private BlogContext dbConnect;


        public MessageFunction(BlogContext newDbconnect)
        {
            dbConnect = newDbconnect;
        }

        public IEnumerable<Message> GetMessage(int idTopic)
        {
            var retrnedValue = dbConnect.Messages.Where(m=>m.IdTopic == idTopic);
            return retrnedValue;
        }
        public IEnumerable<Message> GetAllMessage()
        {
            var retrnedValue = dbConnect.Messages;
            return retrnedValue;
        }

        public void AddMessage(Message message)
        {
            dbConnect.Messages.Add(message);
            dbConnect.SaveChanges();
        }



    }
}