using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Blog.Service;
using Blog.Models;
using Blog.Models.Chatt;

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
            return chatService.GetChatmessages(Id);
        }

        public SendMessage Post(ChatMessage message)
        {
            return chatService.MessageSendedMessage(message,User.Identity.Name);
        }
              

        [Route("api/ajaxchat/create")]
        public SendMessage PostCreateChat(Chat newChat)
        {
            var answer =chatService.MessageForCreateChat(newChat,User.Identity.Name);
            return answer;
        }

        [Route("api/ajaxchat/edit")]
        public SendMessage PostEditChat(Chat newChat)
        {
            return chatService.MessageForEditChatTopic(newChat,User.Identity.Name);
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
            return chatService.MessageForDeleteUser(newUser,User.Identity.Name);
        }

        [Route("api/ajaxchat/delself")]
        public SendMessage PostSelfDel(ChatMessage newUser)
        {
            return chatService.MessageForDeleteUserSelf(newUser,User.Identity.Name);
        }
        #endregion

        #region  Add user to chat

        [Route("api/ajaxchat/addUser")]
        public SendMessage PostAddUserToChat(ChatMessage newUser)
        {
            return chatService.MessageForAddUser(newUser,User.Identity.Name);
        }

        [Route("api/ajaxchat/addadmin")]
        public IEnumerable<SendMessage> GetAddUserToChat(int idChat)
        {
            return chatService.MessageForAddAdminToChat(User.Identity.Name,idChat);
        }

        [Route("api/ajaxchat/addall")]
        public IEnumerable<SendMessage> GetAddAllUserChat(int idChat)
        {
            return chatService.MessageForAddAllUserToChat(User.Identity.Name,idChat);
        }


        #endregion

    }
}
