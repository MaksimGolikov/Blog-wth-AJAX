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
        UserFunction userRepositories;
        TopicFunction topicRepositories;

        public UserService()
        {
            BlogContext DBcontext = new BlogContext();
            userRepositories = new UserFunction(DBcontext);
            topicRepositories = new TopicFunction(DBcontext);
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

            userRepositories.CreateNewUser(us);
            return status;
        }
        public IEnumerable<User> GetUsers()
        {
            return userRepositories.GetUsers();
        }

        public string ChangeUserInformation(User updatedUser)
        {
            string status = "Ok";

            var user = userRepositories.GetUser(updatedUser.Id);
            if (user == null)
            {
                return "Error";
            }
            userRepositories.ChangeUser(updatedUser);
            return status;
        }

        public string LogIn(Login loginData)
        {
            string status = "Ok";

            var user = userRepositories.FindUserByLogin(loginData.LoginName);               
            if (user == null)
            {
                return "Errore";  
            }
            return status;
            
        }

        public User GetUser(string login)
        {
            var user = userRepositories.FindUserByLogin(login);
            return user;
        }

        public User GetUser(User us)
        {
            var user = userRepositories.GetUser(us);
            return user;
        }

        public bool ExistUser(User us)
        {        
            var res =true;
            var user = userRepositories.GetUser(us);

            if(user == null){
                res  = false; 
            }
            return res;
        }
        public bool ExistUser(string UserLogin)
        {
            var res = true;
            var user = userRepositories.FindUserByLogin(UserLogin);

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