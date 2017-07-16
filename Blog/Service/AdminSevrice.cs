using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Entity;
using Blog.Models;
using Blog.Repositories;
using HtmlAgilityPack;

namespace Blog.Service
{
    public class AdminSevrice
    {
        private TopicFunction tableRepositories;
        private UserFunction userRepositories;



        public AdminSevrice()
        {
            BlogContext DBcontext = new BlogContext();
            tableRepositories = new TopicFunction(DBcontext);
            userRepositories = new UserFunction(DBcontext);
        }


        public void AddTopic(Topic newTopic)
        {
            newTopic.PablishingData= DateTime.Now.Date.ToString();
            tableRepositories.AddNewTopic(newTopic);
        }

        public void DeleteTopic(int idTopic)
        {
            tableRepositories.DeleteTopic(idTopic);
        }
        public void ChangeTopic(Topic topic)
        {
            tableRepositories.ChangeTopic(topic);
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


        public string GetContextFromOtherSite(string Url)
        {
            var returnedText = "";      
    
            if(Url != null && Url !="")
            {
                var Webget = new HtmlWeb();
                var doc = Webget.Load(Url);

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//p"))
                {
                    returnedText += node.ChildNodes[0].InnerHtml;
                }
            }
                        

            return returnedText;
        }


    }
}