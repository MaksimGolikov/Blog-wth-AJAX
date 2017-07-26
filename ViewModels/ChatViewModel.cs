using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models.Chatt;
using Blog.Models;

namespace Blog.ViewModels
{
    public class ChatViewModel
    {
        public List<Chat> MyChats {get;set;}
        public List<Chat> ICreated {get;set;}
        public IEnumerable<User> AllUsers {get;set;}
        public ForMasterPage MasterPage { get; set; }
    }
}