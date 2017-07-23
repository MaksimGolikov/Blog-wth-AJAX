using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Entity;
using Blog.Models;
using Blog.Repositories;

namespace Blog.Service
{
    public class MessageService
    {
        private MessageFunction messageRepositories;
        private UserFunction userRepositories;

        public MessageService()
        {
            BlogContext dbContext = new BlogContext();
            messageRepositories = new MessageFunction(dbContext);
            userRepositories = new UserFunction(dbContext);
        }



        public IEnumerable<Message> GetMessageByTopicId(int idTopic)
        {
            var messages = messageRepositories.GetMessage(idTopic);

            foreach(var item in messages)
            {
                item.PablishingData = item.PablishingData.ToLocalTime();
            }

            return messages;
        }
      

        public void CreateMessage(Message newMessage,string userName)
        {
            var user = userRepositories.FindUserByLogin(userName);
            newMessage.UserName = user.FirstName;

            newMessage.PablishingData = DateTime.UtcNow;

            messageRepositories.AddMessage(newMessage);
        }


        public void CreateMessage(Message newMessage)
        {
            newMessage.UserName = "Guest";
            newMessage.PablishingData = DateTime.UtcNow;


            messageRepositories.AddMessage(newMessage);
        }

    }
}