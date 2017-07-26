using Blog.Models;
using Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blog.Controllers.Ajax
{
    public class AjaxMessageController : ApiController
    {
        private MessageService messageService;

        public AjaxMessageController()
        {
            messageService = new MessageService();
        }


        public SendMessage PostMain(Message message)
        {
            if (message.MessageText != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    messageService.CreateMessage(message);
                }
                if (User.Identity.IsAuthenticated)
                {
                    messageService.CreateMessage(message, User.Identity.Name);
                }
            }

                          
            message.PablishingData = DateTime.Now.ToLocalTime();

            SendMessage mess = new SendMessage() { IdTopic = message.IdTopic,
                                                   MessageText = message.MessageText,
                                                   PablishingData = message.PablishingData.ToString("dd-MM-yyyy"),
                                                   UserName = message.UserName };
            
            return mess;
        }



    }
}
