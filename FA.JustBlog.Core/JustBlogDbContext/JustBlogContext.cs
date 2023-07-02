using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.JustBlogDbContext
{
    public class JustBlogContext : DbContext
    {
        public JustBlogContext() : base("JustBlogDbContext")
        {
           Database.SetInitializer<JustBlogContext>(new JustBlogInitializer());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany<Tag>(p => p.Tags)
                .WithMany(t => t.Posts)
                .Map(pt =>
                {
                    pt.MapLeftKey("PostId");
                    pt.MapRightKey("TagId");
                    pt.ToTable("PostTagMap");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
