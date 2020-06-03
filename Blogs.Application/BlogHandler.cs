using Blogs.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Application
{
    public class BlogHandler : IEntityCrudHandler<Blog>
    {
        private readonly IApplicationDbContext db;

        public BlogHandler(IApplicationDbContext db) =>  this.db = db;

        public async Task<int> Alterar(int id, Blog blog, int userID)
        {
            var toAlter = await db.Blogs.SingleOrDefaultAsync(b => b.ID == id);
            if (toAlter != null && toAlter.OwnerID == userID)
            {
                toAlter.Title = blog.Title;
                return await db.SaveChangesAsync();
            }
            return await Task.FromResult(0);
        }

        public async Task<int> Inserir(Blog blog)
        {
            db.Blogs.Add(blog);
            return await db.SaveChangesAsync();
        }

        public async Task<Blog[]> Listar(int userID)
        {
            return await db.Blogs.Where(b=>b.OwnerID == userID).ToArrayAsync();
        }

        public async Task<Blog> ObterUm(int id, int userID)
        {
            return await db.Blogs.Where(b => b.OwnerID == userID).SingleOrDefaultAsync(b=>b.ID == id);
        }

        public async Task<int> Remover(int id, int userID)
        {
            var toRemove = await db.Blogs.SingleOrDefaultAsync(b => b.ID == id);
            if(toRemove != null && toRemove.OwnerID == userID && toRemove.Posts.Count() == 0)
            {
                db.Blogs.Remove(toRemove);
                return await db.SaveChangesAsync();
            }
            return await Task.FromResult(0);
        }
    }
}
