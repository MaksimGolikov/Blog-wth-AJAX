using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;

namespace Blog.ViewModels
{
    public class ShowSelectedViewModel
    {
        public Topic Topic {get;set;}
        public IEnumerable<Message> Comments {get;set;}
        public ForMasterPage MasterPage { get; set; }
    }
}