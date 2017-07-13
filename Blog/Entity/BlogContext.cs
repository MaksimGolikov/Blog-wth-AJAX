using Blog.Entity;
using System.Data.Entity;
using Blog.Models;

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
    }
}