using Blogs.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IEntityCrudHandler<Blog>>(
                serviceProvider => new BlogHandler(
                    serviceProvider.GetService<IApplicationDbContext>()
                )
            );

            services.AddScoped<IEntityCrudHandler<Post>>(
                serviceProvider => new PostHandler(
                    serviceProvider.GetService<IApplicationDbContext>()
                )
            );

            services.AddScoped<UserHandler>(
                serviceProvider => new UserHandler(
                    serviceProvider.GetService<IApplicationDbContext>()
                )
            );

            return services;
        }
    }
}
