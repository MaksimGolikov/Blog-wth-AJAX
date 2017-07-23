using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models.Chat
{
    public class ChatMessage
    {

        public int Id { get; set; }
        public int IdChat { get; set; }
        public string UserName { get; set; }
        public DateTime PablishingData { get; set; }
        public string MessageText { get; set; }


    }
}