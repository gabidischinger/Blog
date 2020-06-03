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
    public class BlogsController : ControllerBase
    {
        private readonly IEntityCrudHandler<Blog> handler;

        public BlogsController(IEntityCrudHandler<Blog> handler)
        {
            this.handler = handler;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var blogs = await handler.Listar(userID);
            return new JsonResult(blogs.Select(b => new { b.ID, b.Title }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var blog = await handler.ObterUm(id, userID);
            return new JsonResult(new { 
                blog.ID, 
                blog.Title,
                Post = blog.Posts.Select(x => x.ID).ToArray(),
                Owner = blog.OwnerID
            });
        }

        [HttpGet("{id}/Posts")]
        public async Task<IActionResult> GetWithPosts(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var blog = await handler.ObterUm(id, userID);
            return new JsonResult(blog.Posts.Select(p => new { p.ID, p.Title }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Blog blog)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            blog.OwnerID = userID;
            await handler.Inserir(blog);
            this.HttpContext.Response.StatusCode = 201;
            return new JsonResult(new { blog.ID });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Blog blog)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            await handler.Alterar(id, blog, userID);
            this.HttpContext.Response.StatusCode = 200;
            var alteredBlog = await handler.ObterUm(id, userID);
            return new JsonResult(new { alteredBlog.ID, alteredBlog.Title });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            await handler.Remover(id, userID);
            this.HttpContext.Response.StatusCode = 200;
            var blogs = await handler.Listar(userID);
            return new JsonResult(blogs.Select(b => new { b.ID, b.Title }));
        }
    }
}
