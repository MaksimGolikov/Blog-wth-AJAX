using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models.AuthModel;
using Blog.Models;

namespace Blog.ViewModels
{
    public class LoginViewModel
    {
        public Login Login {get;set;}
        public ForMasterPage MasterPage { get; set; }
    }
}