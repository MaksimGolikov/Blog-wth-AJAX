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

        private BlogContext dbConnect;

        public UserFunction(BlogContext newDbConnect)
        {
            dbConnect = newDbConnect;
        }



        public User GetUser(User user)
        {
            var retrnedValue = dbConnect.Users.Where(u => u.Login== user.Login && u.Password == user.Password).FirstOrDefault();
            return retrnedValue;
        }

        public User GetUser(int idUser)
        {
            var retrnedValue = dbConnect.Users.Where(u => u.Id == idUser).FirstOrDefault();
            return retrnedValue;
        }
        public IEnumerable<User> GetUsers()
        {
            return dbConnect.Users;
        }

        public User FindUserByLogin(string login)
        {
            var retrnedValue = dbConnect.Users.Where(u => u.Login == login).FirstOrDefault();
            return retrnedValue;
        }
        

        public void CreateNewUser(User user)
        {
            dbConnect.Users.Add(user);
            dbConnect.SaveChanges();
        }

        public void ChangeUser(User user)
        {
            var us = dbConnect.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            us.FirstName = user.FirstName;
            us.SecondName = user.SecondName;
            us.Login = user.Login;
            us.Password = user.Password;

            dbConnect.Entry(us).State = EntityState.Modified;
            dbConnect.SaveChanges();
        }



    }
}