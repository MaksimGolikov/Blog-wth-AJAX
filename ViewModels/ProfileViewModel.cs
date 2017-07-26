using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;

namespace Blog.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public ForMasterPage MasterPage { get; set; }
    }
}