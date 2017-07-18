using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Repositories;
using Blog.Entity;
using Blog.Models;
using HtmlAgilityPack;
using System.Text.RegularExpressions;


namespace Blog.Service
{
    public class TopicService
    {

        private TopicFunction topicRepositiries;
        private MessageFunction messageRepositories;
        private UserFunction userRepositories;



        public TopicService ()
        {
           BlogContext dbContext = new BlogContext();
           topicRepositiries = new TopicFunction(dbContext);
           messageRepositories = new MessageFunction(dbContext);
           userRepositories = new UserFunction(dbContext);
        }


        public IEnumerable<Topic> GetAllTopic()
        {
            var topics = topicRepositiries.GetAllTopic();
            return topics;
        }

        public Topic GetTopic(int idTopic)
        {
           return topicRepositiries.GetTopicContext(idTopic);
        }


        public IEnumerable<Message> GetMessageByTopicId(int idTopic)
        {
            var messages = messageRepositories.GetMessage(idTopic);
            return messages;
        }
        public User GetUser(string login)
        {
            var user = userRepositories.FindUserByLogin(login);
            return user;
        }



        public string GetContextFromOtherSite(string Url)
        {
            var returnedText = "";

            if (Url != null && Url != "")
            {
                var webget = new HtmlWeb();
                var doc = webget.Load(Url);
                var nodes = doc.DocumentNode.SelectNodes("//p");

                if(nodes == null)
                {
                    nodes = doc.DocumentNode.SelectNodes("//pre");
                }
                if(nodes!=null)
                {
                    foreach (HtmlNode item in nodes)
                    {
                        returnedText += item.InnerHtml;
                    }
                }
                returnedText = Regex.Replace(returnedText, "<[^>]+>", string.Empty);
            }
            return returnedText;
        }

    }
}