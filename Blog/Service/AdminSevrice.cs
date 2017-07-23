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
        private TopicFunction topicRepositories;
        private UserFunction userRepositories;



        public AdminSevrice()
        {
            BlogContext dbContext = new BlogContext();
            topicRepositories = new TopicFunction(dbContext);
            userRepositories = new UserFunction(dbContext);
        }


        public void AddTopic(Topic newTopic)
        {
            newTopic.PablishingData = DateTime.UtcNow;
            topicRepositories.AddNewTopic(newTopic);
        }

        public void DeleteTopic(int idTopic)
        {
            topicRepositories.DeleteTopic(idTopic);
        }
        public void ChangeTopic(Topic topic)
        {
            if(topicRepositories.TopicExist(topic))
            {
                topicRepositories.ChangeTopic(topic);
            }           
        }

        public string ChangeUserRole(string newRole, int idUser)
        {
            string status = "Ok";

            var user = userRepositories.GetUser(idUser);
            if (newRole == "")
            {
                return "Error";
            }
            user.Role = newRole;
            userRepositories.ChangeUser(user);
            return status;
        }

              

        public IEnumerable<Topic> GetAllTopic()
        {
            var topics = topicRepositories.GetAllTopic();
            return topics;
        }
        public IEnumerable<User> GetUsers()
        {
            return userRepositories.GetUsers();
        }
        public User GetUser(string login)
        {
            var user = userRepositories.FindUserByLogin(login);
            return user;
        }




    }
}