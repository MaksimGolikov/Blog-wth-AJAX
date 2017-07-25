using Blog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models.Chatt;
using System.Data.Entity;


namespace Blog.Repositories
{
    public class ChatFunction
    {

        private BlogContext dbConnect;

        public ChatFunction(BlogContext newDbconnect)
        {
            dbConnect = newDbconnect;
        }



        public IEnumerable<Chat> GetChats(int idUser)
        {
            var chats = dbConnect.Chats;
            var nes = new List<Chat>();

            foreach(var item in chats)
            {
                var id = item.Users.Split(',');
                var exist = false;
                if(id !=null)
                {
                    foreach (var it in id)
                    {
                        if (it == idUser.ToString())
                        {
                            exist = true;
                            break;
                        }
                    }
                    if (exist)
                    {
                        nes.Add(item);
                    }
                }
                
            }

            return nes;

        }
        public Chat GetChat(int idChat)
        {
            var chat = dbConnect.Chats.Where(ch => ch.ID == idChat).FirstOrDefault();  
            return chat;

        }

        public IEnumerable<Chat> GetCreatedChat(int idUser)
        {
           
            var chat = dbConnect.Chats;
            var returned = new List<Chat>();

            foreach (var item in chat)
            {
                if (item.CreatorID == idUser)
                {
                    returned.Add(item);
                }
            }


           return returned;
        }

       

        public void AddNewChat(Chat newChat)
        {
            dbConnect.Chats.Add(newChat);
            dbConnect.SaveChanges();
        }

       

        public void ChangeChats(Chat chat)
        {    
            var local = dbConnect.Chats.Where(t=>t.ID == chat.ID).FirstOrDefault();
            local.ChatName = chat.ChatName;            
            local.Users=chat.Users;

            dbConnect.Entry(local).State = EntityState.Modified;
            dbConnect.SaveChanges();
        }

        public void DeleteChat(int idChat)
        {
            var t = dbConnect.Chats.Find(idChat);
            dbConnect.Chats.Remove(t);
            dbConnect.SaveChanges();
        }



    }
}