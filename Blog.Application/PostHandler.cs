using Blogs.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application
{
    public class PostHandler : IEntityCrudHandler<Post>
    {
        private readonly IApplicationDbContext db;

        public PostHandler(IApplicationDbContext db) => this.db = db;

        public async Task<int> Alterar(int id, Post post, int userID)
        {
            var toAlter = await db.Posts.SingleOrDefaultAsync(p => p.ID == id);
            if (toAlter != null && toAlter.OwnerID == userID)
            {
                toAlter.Title = post.Title ?? toAlter.Title;
                toAlter.Content = post.Content ?? toAlter.Content;
                toAlter.LastModifiedOn = DateTime.Now;
                return await db.SaveChangesAsync();
            }
            return await Task.FromResult(0);
        }

        public async Task<int> Inserir(Post post)
        {
            post.CreatedOn = DateTime.Now;
            db.Posts.Add(post);
            return await db.SaveChangesAsync();
        }

        public async Task<Post[]> Listar(int userID)
        {
            return await db.Posts.Where(p => p.OwnerID == userID).ToArrayAsync();
        }

        public async Task<Post> ObterUm(int id, int userID)
        {
            return await db.Posts.Where(b => b.OwnerID == userID).SingleOrDefaultAsync(b => b.ID == id);
        }

        public async Task<int> Remover(int id, int userID)
        {
            var toRemove = await db.Posts.SingleOrDefaultAsync(b => b.ID == id);
            if (toRemove != null && toRemove.OwnerID == userID)
            {
                db.Posts.Remove(toRemove);
                return await db.SaveChangesAsync();
            }
            return await Task.FromResult(0);
        }
    }
}
