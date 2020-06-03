using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Application;
using Blogs.Domain;
using JsonWebToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IEntityCrudHandler<Comment> handler;

        public CommentsController(IEntityCrudHandler<Comment> handler)
        {
            this.handler = handler;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var comments = await handler.Listar(userID);
            return new JsonResult(comments.Select(c => new { c.ID, c.PostID, c.Title }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var comment = await handler.ObterUm(id, userID);
            return new JsonResult(new
            {
                comment.ID,
                comment.Title,
                comment.Content,
                Post = comment.Post.Title,
                Name = comment.User.Name,
                CreatedOn = comment.CreatedOn.ToShortDateString()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post(Comment comment)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            comment.UserID = userID;
            await handler.Inserir(comment);
            this.HttpContext.Response.StatusCode = 201;
            return new JsonResult(new { comment.ID });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Comment comment)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            await handler.Alterar(id, comment, userID);
            this.HttpContext.Response.StatusCode = 200;
            var alteredComment = await handler.ObterUm(id, userID);
            return new JsonResult(new { alteredComment.ID, alteredComment.Title, alteredComment.Content });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            await handler.Remover(id, userID);
            this.HttpContext.Response.StatusCode = 200;
            var comments = await handler.Listar(userID);
            return new JsonResult(comments.Select(b => new { b.ID, b.Title }));
        }
    }
}
