using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Application;
using Blogs.Domain;
using JsonWebToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Blogs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IEntityCrudHandler<Post> handler;

        public PostsController(IEntityCrudHandler<Post> handler)
        {
            this.handler = handler;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var posts = await handler.Listar(userID);
            return new JsonResult(posts.Select(b => new { b.ID, b.Title }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var post = await handler.ObterUm(id, userID);
            return new JsonResult(new
            {
                post.ID,
                post.Title,
                post.Content,
                Blog = post.Blog.Title,
                Owner = post.OwnerID,
                CreatedOn = post.CreatedOn.ToShortDateString(),
                LastModifiedOn = post.LastModifiedOn.ToShortDateString()
            });
        }

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetComments(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var post = await handler.ObterUm(id, userID);
            return new JsonResult(post.Comments.Select(c => new { c.ID, c.User.Name ,c.Title }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            post.OwnerID = userID;
            await handler.Inserir(post);
            this.HttpContext.Response.StatusCode = 201;
            return new JsonResult(new { post.ID });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Post post)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            await handler.Alterar(id, post, userID);
            this.HttpContext.Response.StatusCode = 200;
            var alteredPost = await handler.ObterUm(id, userID);
            return new JsonResult(new { alteredPost.ID, alteredPost.Title, alteredPost.Content });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            await handler.Remover(id, userID);
            this.HttpContext.Response.StatusCode = 200;
            var posts = await handler.Listar(userID);
            return new JsonResult(posts.Select(b => new { b.ID, b.Title }));
        }
    }
}
