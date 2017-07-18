using Blog.Models;
using Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blog.Controllers.Ajax
{
    public class AjaxUserController : ApiController
    {
        private UserService userService;

        public AjaxUserController()
        {
            userService = new UserService();
        }


        public string Post(User newUser )
        {
            var returned = false;

            var logins = userService.GetUsers();
            foreach(var item in logins)
            {
                if(item.Login == newUser.Login)
                {
                    returned = true;
                    break;
                }
            }
            if (!returned)
            {
                userService.ChangeUserInformation(newUser);
            }
           

            return returned.ToString();
        }




    }
 }

