using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Entity;
using Blog.Models;
using Blog.Repositories;

namespace Blog.Service
{
    public class AdminSevrice
    {
        private TopicFunction tableRepos;
        private UserFunction userRepos;



        public AdminSevrice()
        {
            BlogContext DBcontext = new BlogContext();
            tableRepos = new TopicFunction(DBcontext);
            userRepos = new UserFunction(DBcontext);
        }


        public void AddTopic(Topic newTopic)
        {
            newTopic.PablishingData= DateTime.Now.Date.ToString();
            tableRepos.AddNewTopic(newTopic);
        }

        public void DeleteTopic(int idTopic)
        {
            tableRepos.DeleteTopic(idTopic);
        }
        public void ChangeTopic(Topic topic)
        {
            tableRepos.ChangeTopic(topic);
        }

        public string ChangeUserRole(string newRole, int idUser)
        {
            string status = "Ok";

            var user = userRepos.GetUser(idUser);
            if (newRole == "")
            {
                return "Error";
            }
            user.Role = newRole;
            userRepos.ChangeUser(user);
            return status;
        }
       

    }
}