using System.Collections.Generic;
using System.Web.Http;
using Blog.Models;
using Blog.Service;



namespace Blog.Controllers
{
    public class AjaxController : ApiController
    {
        private MessageService messageService;
        private TopicService topicService;
        private AdminSevrice adminService;


        public AjaxController()
        {           
           messageService=new MessageService();
           topicService = new TopicService();
           adminService = new AdminSevrice();
        }

       [Route("api/ajax/context")]
        public string PostContent(Message message)
        {
          
           if(message.MessageText == null){               
               return "";           
           }

           return adminService.GetContextFromOtherSite(message.MessageText);
        }
               
        public IEnumerable<Message> PostMain(Message message)
        {                
            if(message.MessageText == null){
                return messageService.GetMessageByTopicId(message.IdTopic);
            }
            

            if (!User.Identity.IsAuthenticated)
            {
                messageService.CreateMessage(message);
            }
            if (User.Identity.IsAuthenticated)
            {
                messageService.CreateMessage(message, User.Identity.Name);
            }
            return messageService.GetMessageByTopicId(message.IdTopic);
        }

        public Topic Get(int id)
        {
            var topic = topicService.GetTopic(id);
            return topic;
        }


    }
}
