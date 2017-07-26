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

        #region Messages

        public SendMessage MessageForAddUser(ChatMessage newUser, string nameWhoAdd)
        {
            var sendMessage = new SendMessage();

            if (newUser != null)
            {
                var user = GetUser(newUser.Id);
                var ShouldAddUser = !ExistUserInChat(newUser.Id, newUser.IdChat);

                if (!ShouldAddUser)
                {
                    string messText = user.Login + " alredy exist at current chat";

                    sendMessage.IdTopic = newUser.IdChat;
                    sendMessage.MessageText = messText;
                    sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy");
                    sendMessage.UserName = nameWhoAdd;
                }
                if (ShouldAddUser)
                {
                    AddUserToChat(newUser.Id, newUser.IdChat);

                    string messText = user.Login + " has been addet to current chat";

                    sendMessage.IdTopic = newUser.IdChat;
                    sendMessage.MessageText = messText;
                    sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy");
                    sendMessage.UserName = nameWhoAdd;


                    var mess = new ChatMessage();
                    mess.IdChat = sendMessage.IdTopic;
                    mess.MessageText = sendMessage.MessageText;
                    mess.PablishingData = DateTime.UtcNow;
                    mess.UserName = sendMessage.UserName;
                    SendMessageToChat(mess, nameWhoAdd);
                }

            }
            return sendMessage;
        }
        public List<SendMessage> MessageForAddAdminToChat(string nameWhoAdd, int idChat)
        {
            var returnedList = new List<SendMessage>();

            var admins = GetAdmins();
            foreach (var item in admins)
            {
                var ShouldAddUser = !ExistUserInChat(item.Id, idChat);

                if (ShouldAddUser)
                {
                    AddUserToChat(item.Id, idChat);

                    string messText = item.Login + " has been addet to current chat";

                    var sendMessage = new SendMessage();
                    sendMessage.IdTopic = idChat;
                    sendMessage.MessageText = messText;
                    sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy");
                    sendMessage.UserName = nameWhoAdd;
                    returnedList.Add(sendMessage);


                    var mess = new ChatMessage();
                    mess.IdChat = sendMessage.IdTopic;
                    mess.MessageText = sendMessage.MessageText;
                    mess.PablishingData = DateTime.UtcNow;
                    mess.UserName = sendMessage.UserName;
                    SendMessageToChat(mess, nameWhoAdd);
                }
            }

            return returnedList;
        }
        public List<SendMessage> MessageForAddAllUserToChat(string nameWhoAdd, int idChat)
        {
            var returnedList = new List<SendMessage>();

            var users = GetAllUsers();
            foreach (var item in users)
            {
                var ShouldAddUser = !ExistUserInChat(item.Id, idChat);

                if (ShouldAddUser)
                {
                    AddUserToChat(item.Id, idChat);

                    string messText = item.Login + " has been addet to current chat";

                    var sendMessage = new SendMessage();
                    sendMessage.IdTopic = idChat;
                    sendMessage.MessageText = messText;
                    sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy");
                    sendMessage.UserName = nameWhoAdd;
                    returnedList.Add(sendMessage);


                    var mess = new ChatMessage();
                    mess.IdChat = sendMessage.IdTopic;
                    mess.MessageText = sendMessage.MessageText;
                    mess.PablishingData = DateTime.UtcNow;
                    mess.UserName = sendMessage.UserName;
                    SendMessageToChat(mess, nameWhoAdd);
                }
            }

            return returnedList;
        }



        public SendMessage MessageForDeleteUserSelf(ChatMessage newUser, string nameWhoAdd)
        {
            var sendMessage = new SendMessage();

            if (newUser != null)
            {
                var user = GetUser(nameWhoAdd);


                DeleteUserFromChat(user.Id, newUser.IdChat);


                string messText = user.Login + " left chat";

                sendMessage.IdTopic = newUser.IdChat;
                sendMessage.MessageText = messText;
                sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy");
                sendMessage.UserName = nameWhoAdd;

                var mess = new ChatMessage();
                mess.IdChat = sendMessage.IdTopic;
                mess.MessageText = sendMessage.MessageText;
                mess.PablishingData = DateTime.UtcNow;
                mess.UserName = sendMessage.UserName;
                SendMessageToChat(mess, nameWhoAdd);
            }
            return sendMessage;
        }
        public SendMessage MessageForDeleteUser(ChatMessage newUser, string nameWhoAdd)
        {
            var sendMessage = new SendMessage();

            if (newUser != null)
            {
                DeleteUserFromChat(newUser.Id, newUser.IdChat);
                var user = GetUser(newUser.Id);

                string messText = user.Login + " left chat";

                sendMessage.IdTopic = newUser.IdChat;
                sendMessage.MessageText = messText;
                sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy");
                sendMessage.UserName = nameWhoAdd;

                var mess = new ChatMessage();
                mess.IdChat = sendMessage.IdTopic;
                mess.MessageText = sendMessage.MessageText;
                mess.PablishingData = DateTime.UtcNow;
                mess.UserName = sendMessage.UserName;
                SendMessageToChat(mess, nameWhoAdd);
            }
            return sendMessage;
        }



        public SendMessage MessageForEditChatTopic(Chat newChat, string nameWhoAdd)
        {
            var sendMessage = new SendMessage();

            if (newChat != null && newChat.ChatName != "")
            {
                var existName = GetChat(newChat.ID).ChatName;
                EditChat(newChat);
                

                string messText = "Topic of chat has been changed from " + existName + " to " + newChat.ChatName;

                sendMessage.IdTopic = newChat.ID;
                sendMessage.MessageText = messText;
                sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy");
                sendMessage.UserName = nameWhoAdd;

                var mess = new ChatMessage();
                mess.IdChat = sendMessage.IdTopic;
                mess.MessageText = sendMessage.MessageText;
                mess.PablishingData = DateTime.UtcNow;
                mess.UserName = sendMessage.UserName;
                SendMessageToChat(mess, nameWhoAdd);
            }
            return sendMessage;
        }

        public SendMessage MessageForCreateChat(Chat newChat, string nameWhoAdd)
        {
            var sendMessage = new SendMessage();

            if (newChat != null)
            {
                var idCreatedChat = CreateChat(newChat, nameWhoAdd);

                string messText = "Chat " + newChat.ChatName + " has been created";

                sendMessage.IdTopic = idCreatedChat;
                sendMessage.MessageText = messText;
                sendMessage.PablishingData = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy");
                sendMessage.UserName = nameWhoAdd;

                var mess = new ChatMessage();
                mess.IdChat = sendMessage.IdTopic;
                mess.MessageText = sendMessage.MessageText;
                mess.PablishingData = DateTime.UtcNow;
                mess.UserName = sendMessage.UserName;
                SendMessageToChat(mess, nameWhoAdd);
            }
            return sendMessage;
        }

        public SendMessage MessageSendedMessage(ChatMessage message, string nameWhoAdd)
        {
            if (message.MessageText != null)
            {
                SendMessageToChat(message, nameWhoAdd);
            }

            message.PablishingData = DateTime.Now.ToLocalTime();

            SendMessage mess = new SendMessage()
            {
                IdTopic = message.IdChat,
                MessageText = message.MessageText,
                PablishingData = message.PablishingData.ToString("dd-MM-yyyy"),
                UserName = message.UserName
            };

            return mess;
        }

        public List<SendMessage> GetChatmessages(int Id)
        {
            var users = GetAllUsers();
            var freeUsers = new List<User>();
            foreach (var item in users)
            {
                var ShouldAddUser = !ExistUserInChat(item.Id, Id);
                if (ShouldAddUser)
                {
                    freeUsers.Add(item);
                }
            }

            var usersAtChat = GetChat(Id);
            var usedUsers = usersAtChat.Users.Split(',');
            var busyUsers = new List<User>();
            foreach (var item in usedUsers)
            {
                busyUsers.Add(GetUser(item));
            }


            var message = GetMessageByChatcId(Id);
            var sendMessage = new List<SendMessage>();
            foreach (var item in message)
            {
                item.PablishingData = item.PablishingData.ToLocalTime();



                SendMessage mess = new SendMessage()
                {
                    IdTopic = item.IdChat,
                    MessageText = item.MessageText,
                    PablishingData = item.PablishingData.ToString("dd-MM-yyyy"),
                    UserName = item.UserName,
                    NameChat = GetChat(item.IdChat).ChatName
                };
                sendMessage.Add(mess);
            }



            sendMessage[0].FreeUser = freeUsers;
            sendMessage[0].UserAtChat = busyUsers;

            return sendMessage;
        }

        #endregion

    }
}