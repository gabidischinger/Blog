using Blogs.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application
{
    public class CommentHandler : IEntityCrudHandler<Comment>
    {
        private readonly IApplicationDbContext db;

        public CommentHandler(IApplicationDbContext db) => this.db = db;

        public async Task<int> Alterar(int id, Comment comment, int userID)
        {
            var toAlter = await db.Comments.SingleOrDefaultAsync(b => b.ID == id);
            if (toAlter != null && toAlter.UserID == userID)
            {
                toAlter.Title = comment.Title ?? toAlter.Title;
                toAlter.Content = comment.Content ?? toAlter.Content;
                return await db.SaveChangesAsync();
            }
            return await Task.FromResult(0);
        }

        public async Task<int> Inserir(Comment comment)
        {
            db.Comments.Add(comment);
            return await db.SaveChangesAsync();
        }

        public async Task<Comment[]> Listar(int userID)
        {
            return await db.Comments.Where(c => c.UserID == userID).ToArrayAsync();
        }

        public async Task<Comment> ObterUm(int id, int userID)
        {
            return await db.Comments.Where(c => c.UserID == userID).SingleOrDefaultAsync(c => c.ID == id);
        }

        public async Task<int> Remover(int id, int userID)
        {
            var toRemove = await db.Comments.SingleOrDefaultAsync(c => c.ID == id);
            if (toRemove != null && toRemove.UserID == userID)
            {
                db.Comments.Remove(toRemove);
                return await db.SaveChangesAsync();
            }
            return await Task.FromResult(0);
        }
    }
}
