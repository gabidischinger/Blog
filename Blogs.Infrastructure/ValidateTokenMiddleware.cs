using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JsonWebToken;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Biblioteca.Infrastructure
{
    public class ValidateTokenMiddleware
    {
        private readonly RequestDelegate next;

        public ValidateTokenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                try
                {
                    var secret = configuration.GetValue<string>("JWTKey");

                    var token = context.Request.Headers["Authorization"];

                    JWTPayload payload;

                    if (JWT.VerifyToken<JWTPayload>(token, secret, out payload))
                    {
                        context.Items["JWTPayload"] = payload;
                    }
                }
                catch (Exception) { }
            }
            await next.Invoke(context);
        }
    }
}
