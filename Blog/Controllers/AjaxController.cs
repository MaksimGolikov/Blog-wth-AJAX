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

        public AjaxController()
        {           
           messageService=new MessageService();
           topicService = new TopicService();
        }

               
        public IEnumerable<Message> Post(Message message)
        {           
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
