using Blog.Models.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;

namespace Blog.ViewModels
{
    public class AuthenticationViewModel
    {
        public Registred Registred {get;set;}
        public ForMasterPage MasterPage { get; set; }
    }
}