using Blog.Entity;
using Blog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models.Chatt;
using Blog.Models;



namespace Blog.Service
{
    public class ChatService
    {
        private ChatFunction ChatRepositiries;
        private ChatMessageFunction messageRepositories;
        private UserFunction userRepositories;


        public ChatService()
        {
           BlogContext dbContext = new BlogContext();
           ChatRepositiries = new ChatFunction(dbContext);
           messageRepositories = new ChatMessageFunction(dbContext);
           userRepositories = new UserFunction(dbContext);           
        }


        public IEnumerable<Chat> GetAllUsedChats(int idUser)
        {
            var chats = ChatRepositiries.GetChats(idUser);
            return chats;
        }

        public IEnumerable<Chat> GetAllCreatedChat(int idUser)
        {
            var chats = ChatRepositiries.GetCreatedChat(idUser);
            return chats;
        }


        public Chat GetChat(int idChat)
        {
            var chat = ChatRepositiries.GetChat(idChat);            
            return chat;
        }


        public void DeleteChat(int idChat)
        {
            ChatRepositiries.DeleteChat(idChat);
        }
        public int CreateChat(Chat newChat, string loginCreator)
        {
            var idCreated = 0;

            if(newChat !=null && loginCreator != null)
            {
                var creator = userRepositories.FindUserByLogin(loginCreator);
                newChat.CreatorID = creator.Id;
                newChat.Users = creator.Id.ToString();

                ChatRepositiries.AddNewChat(newChat);


                var chats = ChatRepositiries.GetChats(creator.Id) as List<Chat>;
                idCreated = chats[chats.Count - 1].ID;
            }
            return idCreated;
        }

        


        public void SendMessageToChat(ChatMessage message, string userName)
        {
            if(message != null)
            {
                var user = userRepositories.FindUserByLogin(userName);
                message.UserName = user.FirstName;
         
                message.PablishingData = DateTime.UtcNow;
                messageRepositories.AddMessage(message);
            }
        }


        public void AddUserToChat(int addUserId,int idChat)
        {
            var us = userRepositories.GetUser(addUserId);
            var chat = ChatRepositiries.GetChat(idChat);

            chat.Users = chat.Users + "," + us.Id;
            ChatRepositiries.ChangeChats(chat);
        }
        public void DeleteUserFromChat(int idUser, int idChat)
        {
            var chat = ChatRepositiries.GetChat(idChat);

            var user = chat.Users.Split(',');
            var changeListuser = "";
            for (int i = 0; i < user.Length; i++)
            {
                if(user[i] != idUser.ToString() )
                {
                    changeListuser += user[i];
                    if (i != user.Length - 1)
                    {
                        changeListuser += ",";
                    }
                }
            }

            chat.Users = changeListuser;
            ChatRepositiries.ChangeChats(chat);
        }



        public IEnumerable<ChatMessage> GetMessageByChatcId(int idChat)
        {
            var messages = messageRepositories.GetMessage(idChat);
            foreach (var item in messages)
            {
                item.PablishingData = item.PablishingData.ToLocalTime();
            }

            return messages;
        }

        public User GetUser(string login)
        {
            var user = userRepositories.FindUserByLogin(login);
            return user;
        }
        public User GetUser(int idUser)
        {
            var user = userRepositories.GetUser(idUser);
            return user;
        }
        public IEnumerable<User> GetAllUsers()
        {
            var users = userRepositories.GetUsers();
            return users;
        }
        public IEnumerable<User> GetAdmins()
        {
            var admins = userRepositories.GetAdmins();
            return admins;
        }

        public void EditChat(Chat newChat)
        {
            var existChat = ChatRepositiries.GetChat(newChat.ID);

            existChat.ChatName = newChat.ChatName;
            ChatRepositiries.ChangeChats(existChat);
        }



        public bool ExistUserInChat(int idUser,int idChat)
        {
            var chat = ChatRepositiries.GetChat(idChat);
            var existUsers = chat.Users.Split(',');
            var ansver = false;

            foreach(var item in existUsers)
            {
                if (item == idUser.ToString())
                {
                    ansver = true;
                    break;
                }
            }
            return ansver;
        }



    }
}