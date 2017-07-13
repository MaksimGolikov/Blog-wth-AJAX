using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;
using Blog.Entity;
using System.Data.Entity;


namespace Blog.Repositories
{
    public class UserFunction
    {

        private BlogContext DBconnect;

        public UserFunction(BlogContext newDBconnect)
        {
            DBconnect = newDBconnect;
        }



        public User GetUser(User user)
        {
            var retrnedValue = DBconnect.Users.Where(u => u.Login== user.Login && u.Password == user.Password).FirstOrDefault();
            return retrnedValue;
        }

        public User GetUser(int idUser)
        {
            var retrnedValue = DBconnect.Users.Where(u => u.Id == idUser).FirstOrDefault();
            return retrnedValue;
        }
        public IEnumerable<User> GetUsers()
        {
            return DBconnect.Users;
        }

        public User FindUserByLogin(string login)
        {
            var retrnedValue = DBconnect.Users.Where(u => u.Login == login).FirstOrDefault();
            return retrnedValue;
        }
        

        public void CreateNewUser(User user)
        {           
            DBconnect.Users.Add(user);
            DBconnect.SaveChanges();
        }

        public void ChangeUser(User user)
        {
            var us = DBconnect.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            us.FirstName = user.FirstName;
            us.SecondName = user.SecondName;
            us.Login = user.Login;
            us.Password = user.Password;

            DBconnect.Entry(us).State = EntityState.Modified;
            DBconnect.SaveChanges();
        }



    }
}