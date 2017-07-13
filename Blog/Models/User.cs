using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class User
    {
        public int  Id { get; set;}
      
        public string FirstName { get; set; }
      
        public string SecondName { get; set; }
        
        public string Login { get; set; }
       
        public string Password { get; set; }
        public string Role { get; set; }
    }
}