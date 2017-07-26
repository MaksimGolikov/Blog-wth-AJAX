using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;

namespace Blog.ViewModels
{
    public class FirstPageViewModel
    {
        public IEnumerable<Topic> Topics {get;set;}
        public ForMasterPage MasterPage { get; set; }
    }
}