using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string NameTopic { get; set; }
        public string ContextTopic { get; set; }
        public DateTime PablishingData { get; set; }
    }
}