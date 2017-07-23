using Blog.Entity;
using System.Data.Entity;
using Blog.Models;
using Blog.Models.Chat;

namespace Blog.Entity
{
    public class BlogContext: DbContext
    {
        static BlogContext()
        {
            Database.SetInitializer<BlogContext>(new BlogContextInitializer());
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

    }
}