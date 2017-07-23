using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Blog.Service;
using Blog.Models;
using Blog.Models.Chat;

namespace Blog.Controllers
{
    public class AjaxChatController : ApiController
    {

        private ChatService chatService;

        public AjaxChatController()
        {
            chatService = new ChatService();
        }



        public List<SendMessage> Get(int Id)
        {
           
            var users = chatService.GetAllUsers();
            var freeUsers = new List<User>();
            foreach(var item in users)
            {
                var ShouldAddUser = !chatService.ExistUserInChat(item.Id, Id);
                if(ShouldAddUser)
                {
                    freeUsers.Add(item);
                }
            }

            var usersAtChat = chatService.GetChat(Id);
            var usedUsers = usersAtChat.Users.Split(',');
            var busyUsers = new List<User>();
            foreach (var item in usedUsers)
            {
               busyUsers.Add(chatService.GetUser(item));                
            }


            var message = chatService.GetMessageByChatcId(Id);
            var sendMessage = new List<SendMessage>();
            foreach(var item in message)
            {
                item.PablishingData = item.PablishingData.ToLocalTime();                



                SendMessage mess = new SendMessage() {  IdTopic = item.IdChat,
                                                        MessageText = item.MessageText,
                                                        PablishingData = item.PablishingData.ToString(),
                                                        UserName = item.UserName,
                                                        NameChat = chatService.GetChat(item.IdChat).ChatName
                                                    };
                sendMessage.Add(mess);
            }

            

            sendMessage[0].FreeUser = freeUsers;
            sendMessage[0].UserAtChat = busyUsers;

            return sendMessage;
        }

        public SendMessage Post(ChatMessage message)
        {
            if (message.MessageText != null)
            {
                chatService.SendMessageToChat(message, User.Identity.Name);                
            }
            
            message.PablishingData = DateTime.Now.ToLocalTime();

            SendMessage mess = new SendMessage()
            {
                IdTopic = message.IdChat,
                MessageText = message.MessageText,
                PablishingData = message.PablishingData.ToString(),
                UserName = message.UserName
            };

            return mess;
        }
              

        [Route("api/ajaxchat/create")]
        public SendMessage PostCreateChat(Chat newChat)
        {
            var sendMessage = new SendMessage();

            if(newChat !=null )
            {
               var idCreatedChat = chatService.CreateChat(newChat,User.Identity.Name);
                                  
                string messText =   "Chat "+newChat.ChatName +" has been created"; 

                sendMessage.IdTopic = idCreatedChat;
                sendMessage.MessageText = messText;
                sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString();
                sendMessage.UserName = User.Identity.Name;

                var mess = new ChatMessage();
                   mess.IdChat = sendMessage.IdTopic;
                   mess.MessageText = sendMessage.MessageText;
                   mess.PablishingData = DateTime.UtcNow;
                   mess.UserName = sendMessage.UserName;
                   chatService.SendMessageToChat(mess, User.Identity.Name);    
            }    
             return sendMessage;
        }

        [Route("api/ajaxchat/edit")]
        public SendMessage PostEditChat(Chat newChat)
        {
            var sendMessage = new SendMessage();

            if (newChat != null && newChat.ChatName!="")
            {
                var existName = chatService.GetChat(newChat.ID).ChatName;
                chatService.EditChat(newChat);
                


                string messText = "Topic of chat has been changed from " + existName + " to " + newChat.ChatName;

                sendMessage.IdTopic = newChat.ID;
                sendMessage.MessageText = messText;
                sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString();
                sendMessage.UserName = User.Identity.Name;

                var mess = new ChatMessage();
                mess.IdChat = sendMessage.IdTopic;
                mess.MessageText = sendMessage.MessageText;
                mess.PablishingData = DateTime.UtcNow;
                mess.UserName = sendMessage.UserName;
                chatService.SendMessageToChat(mess, User.Identity.Name);
            }
            return sendMessage;
        }


        #region Delete
        [Route("api/ajaxchat/del")]
        public string PostDeleteChat(ChatMessage chat)
        {
            if (chat.IdChat != 0)
            {
                chatService.DeleteChat(chat.IdChat);
            }
            return "";
        }

        [Route("api/ajaxchat/deluser")]
        public SendMessage PostDelUseer(ChatMessage newUser)
        {
            var sendMessage = new SendMessage();

            if (newUser != null)
            {
                chatService.DeleteUserFromChat(newUser.Id, newUser.IdChat);
                var us = chatService.GetUser(newUser.Id);

                string messText = us.Login+" left chat";

                sendMessage.IdTopic = newUser.IdChat;
                sendMessage.MessageText = messText;
                sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString();
                sendMessage.UserName = User.Identity.Name;

                var mess = new ChatMessage();
                mess.IdChat = sendMessage.IdTopic;
                mess.MessageText = sendMessage.MessageText;
                mess.PablishingData = DateTime.UtcNow;
                mess.UserName = sendMessage.UserName;
                chatService.SendMessageToChat(mess, User.Identity.Name);
            }
            return sendMessage;
        }

        [Route("api/ajaxchat/delself")]
        public SendMessage PostSelfDel(ChatMessage newUser)
        {
            var sendMessage = new SendMessage();

            if (newUser != null)
            {
                var us = chatService.GetUser(User.Identity.Name);


                chatService.DeleteUserFromChat(us.Id, newUser.IdChat);
               

                string messText = us.Login + " left chat";

                sendMessage.IdTopic = newUser.IdChat;
                sendMessage.MessageText = messText;
                sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString();
                sendMessage.UserName = User.Identity.Name;

                var mess = new ChatMessage();
                mess.IdChat = sendMessage.IdTopic;
                mess.MessageText = sendMessage.MessageText;
                mess.PablishingData = DateTime.UtcNow;
                mess.UserName = sendMessage.UserName;
                chatService.SendMessageToChat(mess, User.Identity.Name);
            }
            return sendMessage;
        }
        #endregion

        #region  Add user to chat
        [Route("api/ajaxchat/addUser")]
        public SendMessage PostAddUserToChat(ChatMessage newUser)
        {
            var sendMessage = new SendMessage();

            if (newUser != null)
            {
                var user = chatService.GetUser(newUser.Id);
                var ShouldAddUser = !chatService.ExistUserInChat(newUser.Id, newUser.IdChat);

                if (!ShouldAddUser)
                {                    
                    string messText = user.Login + " alredy exist at current chat";

                    sendMessage.IdTopic = newUser.IdChat;
                    sendMessage.MessageText = messText;
                    sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString();
                    sendMessage.UserName = User.Identity.Name;
                }
                if (ShouldAddUser)
                {
                    chatService.AddUserToChat(newUser.Id, newUser.IdChat);                   

                    string messText = user.Login + " has been addet to current chat";

                    sendMessage.IdTopic = newUser.IdChat;
                    sendMessage.MessageText = messText;
                    sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString();
                    sendMessage.UserName = User.Identity.Name;


                    var mess = new ChatMessage();
                    mess.IdChat = sendMessage.IdTopic;
                    mess.MessageText = sendMessage.MessageText;
                    mess.PablishingData = DateTime.UtcNow;
                    mess.UserName = sendMessage.UserName;
                    chatService.SendMessageToChat(mess, User.Identity.Name); 
                }
               
            }
            return sendMessage;
        }

        [Route("api/ajaxchat/addadmin")]
        public IEnumerable<SendMessage> GetAddUserToChat(int idChat)
        {
            var returnedList = new List<SendMessage>();
            
            var admins = chatService.GetAdmins();
            foreach(var item in admins)
            {
                var ShouldAddUser = !chatService.ExistUserInChat(item.Id, idChat);

                if (ShouldAddUser)
                {
                    chatService.AddUserToChat(item.Id, idChat);

                    string messText = item.Login + " has been addet to current chat";

                    var sendMessage = new SendMessage();
                    sendMessage.IdTopic = idChat;
                    sendMessage.MessageText = messText;
                    sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString();
                    sendMessage.UserName = User.Identity.Name;
                    returnedList.Add(sendMessage);


                    var mess = new ChatMessage();
                    mess.IdChat = sendMessage.IdTopic;
                    mess.MessageText = sendMessage.MessageText;
                    mess.PablishingData = DateTime.UtcNow;
                    mess.UserName = sendMessage.UserName;
                    chatService.SendMessageToChat(mess, User.Identity.Name);
                }
            }

           return returnedList;
        }

        [Route("api/ajaxchat/addall")]
        public IEnumerable<SendMessage> GetAddAllUserChat(int idChat)
        {
            var returnedList = new List<SendMessage>();

            var users = chatService.GetAllUsers();
            foreach (var item in users)
            {
                var ShouldAddUser = !chatService.ExistUserInChat(item.Id, idChat);

                if (ShouldAddUser)
                {
                    chatService.AddUserToChat(item.Id, idChat);

                    string messText = item.Login + " has been addet to current chat";

                    var sendMessage = new SendMessage();
                    sendMessage.IdTopic = idChat;
                    sendMessage.MessageText = messText;
                    sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString();
                    sendMessage.UserName = User.Identity.Name;
                    returnedList.Add(sendMessage);


                    var mess = new ChatMessage();
                    mess.IdChat = sendMessage.IdTopic;
                    mess.MessageText = sendMessage.MessageText;
                    mess.PablishingData = DateTime.UtcNow;
                    mess.UserName = sendMessage.UserName;
                    chatService.SendMessageToChat(mess, User.Identity.Name);
                }
            }

            return returnedList;
        }


        #endregion

    }
}
