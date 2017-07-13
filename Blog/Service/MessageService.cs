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
        private MessageFunction messageRepos;
        private UserFunction userRepos;

        public MessageService()
        {
            BlogContext DBcontext = new BlogContext();
            messageRepos = new MessageFunction(DBcontext);
            userRepos = new UserFunction(DBcontext);
        }



        public IEnumerable<Message> GetMessageByTopicId(int idTopic)
        {
            var messages = messageRepos.GetMessage(idTopic);
            return messages;
        }

        public void CreateMessage(Message newMessage,string userName)
        {
            var user = userRepos.FindUserByLogin(userName);
            newMessage.UserName = user.FirstName;
            messageRepos.AddMessage(newMessage);
        }
        public void CreateMessage(Message newMessage)
        {
            newMessage.UserName = "Guest";
            messageRepos.AddMessage(newMessage);
        }

    }
}