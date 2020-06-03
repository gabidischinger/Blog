using Blogs.Application;
using Blogs.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Blogs.Persistency
{
    public class BlogsDbContext : DbContext, IApplicationDbContext
    {
        public BlogsDbContext(DbContextOptions<BlogsDbContext> options): base(options)
        {
        }
        
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().HasKey(b => b.ID);
            modelBuilder.Entity<Blog>().Property(b => b.ID).ValueGeneratedOnAdd();

            modelBuilder.Entity<User>().HasKey(u => u.ID);
            modelBuilder.Entity<User>().Property(u => u.ID).ValueGeneratedOnAdd();

            modelBuilder.Entity<Post>().HasKey(p => p.ID);
            modelBuilder.Entity<Post>().Property(p => p.ID).ValueGeneratedOnAdd();

            modelBuilder.Entity<Comment>().HasKey(c => c.ID);
            modelBuilder.Entity<Comment>().Property(c => c.ID).ValueGeneratedOnAdd();

            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Posts)
                .WithOne(p => p.Blog)
                .HasForeignKey(p => p.BlogID);

            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Owner)
                .WithMany(o => o.Blogs)
                .HasForeignKey(b => b.OwnerID);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Posts)
                .HasForeignKey(p => p.OwnerID);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostID);




            modelBuilder.Entity<User>().HasData(new User[] { 
                new User(){ ID = 1, Name = "Bruno", ApiKey = Guid.NewGuid().ToString(), ApiSecret = Guid.NewGuid().ToString()},
                new User(){ ID = 2, Name = "Gabi", ApiKey = Guid.NewGuid().ToString(), ApiSecret = Guid.NewGuid().ToString()},
                new User(){ ID = 3, Name = "Nohan", ApiKey = Guid.NewGuid().ToString(), ApiSecret = Guid.NewGuid().ToString()},
                new User(){ ID = 4, Name = "Ricardo", ApiKey = Guid.NewGuid().ToString(), ApiSecret = Guid.NewGuid().ToString()}
            });
        }
    }
}
