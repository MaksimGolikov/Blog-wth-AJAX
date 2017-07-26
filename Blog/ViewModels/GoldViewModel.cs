using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;

namespace Blog.ViewModels
{
    public class GoldViewModel
    {
       public string UserName {get;set;}
       public ForMasterPage MasterPage { get; set; }
    }
}