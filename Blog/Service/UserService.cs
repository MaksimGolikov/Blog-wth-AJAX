using Blog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Repositories;
using Blog.Models;
using Blog.Models.AuthModel;

namespace Blog.Service
{
    public class UserService
    {
        UserFunction userRepos;
        TopicFunction topicRepos;

        public UserService()
        {
            BlogContext DBcontext = new BlogContext();
            userRepos = new UserFunction(DBcontext);
            topicRepos = new TopicFunction(DBcontext);
        }



        public string CreateNewUser(Registred newUser)
        {
            string status = "Ok";

            if (newUser == null)
            {
                return "Error";
            }
            var us = new User();
            us.FirstName = newUser.FirstName;
            us.SecondName = newUser.SecondName;
            us.Login = newUser.Login;
            us.Password = newUser.Password;
            us.Role = "user";

            userRepos.CreateNewUser(us);
            return status;
        }
        public IEnumerable<User> GetUsers()
        {
            return userRepos.GetUsers();
        }

        public string ChangeUserInformation(User updatedUser)
        {
            string status = "Ok";

            var user = userRepos.GetUser(updatedUser.Id);
            if (user == null)
            {
                return "Error";
            }
            userRepos.ChangeUser(updatedUser);
            return status;
        }

        public string LogIn(Login loginData)
        {
            string status = "Ok";

            var user = userRepos.FindUserByLogin(loginData.LoginName);               
            if (user == null)
            {
                return "Errore";  
            }
            return status;
            
        }

        public User GetUser(string login)
        {
            var user = userRepos.FindUserByLogin(login);
            return user;
        }

        public User GetUser(User us)
        {
            var user = userRepos.GetUser(us);
            return user;
        }

        public bool ExistUser(User us)
        {        
            var res =true;
            var user = userRepos.GetUser(us);

            if(user == null){
                res  = false; 
            }
            return res;
        }
        public bool ExistUser(string UserLogin)
        {
            var res = true;
            var user = userRepos.FindUserByLogin(UserLogin);

            if (user == null)
            {
                res = false;
            }
            return res;
        }

        public Login GetLoginUserProfile(User us)
        {
            var loginAccount = new Login();
            loginAccount.LoginName = us.Login;
            loginAccount.Password = us.Password;
            loginAccount.Role = us.Role;

            return loginAccount;
        }



    }
}