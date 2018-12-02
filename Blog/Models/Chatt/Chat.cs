using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models.Chatt
{
    public class Chat
    {
        public int ID {get;set;}
        public string ChatName { get; set; }
        public int CreatorID { get; set; }
        public string  Users { get; set; }
        
    }
}