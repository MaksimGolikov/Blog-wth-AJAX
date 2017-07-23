using Blog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models.Chat;

namespace Blog.Repositories
{
    public class ChatMessageFunction
    {

        private BlogContext dbConnect;


        public ChatMessageFunction(BlogContext newDbconnect)
        {
            dbConnect = newDbconnect;
        }

        public IEnumerable<ChatMessage> GetMessage(int idChat)
        {
            var retrnedValue = dbConnect.ChatMessages.Where(m=>m.IdChat == idChat);
            return retrnedValue;
        }
       
        public void AddMessage(ChatMessage message)
        {
            dbConnect.ChatMessages.Add(message);
            dbConnect.SaveChanges();
        }
    }
}