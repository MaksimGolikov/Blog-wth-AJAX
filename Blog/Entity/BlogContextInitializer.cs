using Blog.Entity;
using System;
using System.Data.Entity;
using Blog.Models;


namespace Blog.Entity
{
    public class BlogContextInitializer : CreateDatabaseIfNotExists<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            User admin = new User {Id =1,
                                   Role ="admin",
                                   FirstName ="admin",
                                   SecondName ="admin",
                                   Login ="Embedded+Web",
                                   Password ="1995_G" };           
            User guest = new User { Id = 2,
                                    Role = "guest",
                                    FirstName = "",
                                    SecondName = "",
                                    Login = "",
                                    Password = ""
                                    };
            User odinary = new User { Id = 3,
                                      Role = "user",
                                      FirstName = "Maxim",
                                      SecondName = "Golikov",
                                      Login = "User",
                                      Password = "Password"
                                        };
            context.Users.Add(admin);
            context.Users.Add(odinary);
            context.Users.Add(guest);

            Topic topic = new Topic {Id = 1,
                                     NameTopic ="Start blog",
                                     PablishingData = DateTime.Now.ToShortDateString(),
                                     ContextTopic ="Hi, my name is Maksim and I`ll try to write this blog. I`ll write some interest information about programming for embeddeds system" };
            context.Topic.Add(topic);


            Message message = new Message { IdTopic = 1,
                                            UserName = "Maxim",
                                            PablishingData = DateTime.Now.ToShortDateString(),
                                            MessageText =" This is a first publishing in my blog. If you have any interest fact, please write to me. You can finde my e-mail in tab 'About me'. " };
            context.Messages.Add(message);
                       

            context.SaveChanges();
        }


    }
}