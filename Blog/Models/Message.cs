using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int IdTopic { get; set; }
        public string UserName { get; set; }
        public string PablishingData { get; set; }
        public string MessageText { get; set; }

    }
}