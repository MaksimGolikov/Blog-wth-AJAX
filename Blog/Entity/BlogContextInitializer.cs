using Blog.Entity;
using System;
using System.Data.Entity;
using Blog.Models;
using Blog.Models.Chatt;
using System.Collections.Generic;

namespace Blog.Entity
{
    public class BlogContextInitializer : DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            var admin = new User { Id =1,
                                   Role ="admin",
                                   FirstName ="admin",
                                   SecondName ="admin",
                                   Login ="admin",
                                   Password ="1995_G" };           
            var guest = new User {  Id = 2,
                                    Role = "guest",
                                    FirstName = "",
                                    SecondName = "",
                                    Login = "",
                                    Password = ""
                                    };
            var odinary = new User {  Id = 3,
                                      Role = "user",
                                      FirstName = "Maxim",
                                      SecondName = "Golikov",
                                      Login = "User",
                                      Password = "Password"
                                        };
            context.Users.Add(admin);
            context.Users.Add(odinary);
            context.Users.Add(guest);

            var topic = new Topic { Id = 1,
                                    NameTopic ="Start blog",
                                    PablishingData = DateTime.UtcNow,
                                    ContextTopic ="Hi, my name is Maksim and I`ll try to write this blog. I`ll write some interest information about programming for embeddeds system" };
            context.Topic.Add(topic);


            var message = new Message { IdTopic = 1,
                                        UserName = "Maxim",
                                        PablishingData = DateTime.UtcNow,
                                        MessageText =" This is a first publishing in my blog. If you have any interest fact, please write to me. You can finde my e-mail in tab 'About me'. " };
            context.Messages.Add(message);

                        
            var c1 = new Chat { ID=1,
                                CreatorID = 3,
                                ChatName = "test 1",
                                Users = "1,2,3"
                            };
            var c2 = new Chat { ID=2,
                                CreatorID = 1,
                                ChatName = "test 2",
                                Users = "1,3"
                            };
            context.Chats.Add(c1);
            context.Chats.Add(c2);

            var ChatMessage1 = new ChatMessage { IdChat = 1,
                                                 MessageText="chat 1 start",
                                                 PablishingData = DateTime.UtcNow,
                                                 UserName = "admin"
                                                };
            var ChatMessage2 = new ChatMessage  { IdChat = 2,
                                                  MessageText = "chat 2 start",
                                                  PablishingData = DateTime.UtcNow,
                                                  UserName = " "
                                                };
            context.ChatMessages.Add(ChatMessage1);
            context.ChatMessages.Add(ChatMessage2);



            context.SaveChanges();
        }


    }
}