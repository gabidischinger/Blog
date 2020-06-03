using Blogs.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Persistency
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistency(this IServiceCollection services)
        {
            services.AddDbContext<BlogsDbContext>(options =>
            {
                options.UseSqlite("Filename=../Blogs.Persistency/Blogs.db", opt =>
                {
                    opt.MigrationsAssembly(
                        typeof(BlogsDbContext).Assembly.FullName
                    );
                });

                options.UseLazyLoadingProxies();
            });

            services.AddScoped<IApplicationDbContext>(ctx =>
                ctx.GetRequiredService<BlogsDbContext>());

            return services;
        }
    }
}
