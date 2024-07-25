using Prosigliere.SimpleBlog.Application;
using Prosigliere.SimpleBlog.Application.UseCases.BlogPost.CreateBlogPost;
using Prosigliere.SimpleBlog.Domain.Repository;
using Prosigliere.SimpleBlog.Infra.Data.EF;
using Prosigliere.SimpleBlog.Infra.Data.EF.Repositories;

namespace Prosigliere.SimpleBlog.Api.Configurations;

public static class UseCaseConfiguration
{
    public static IServiceCollection AddUseCases(
               this IServiceCollection services
           )
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateBlogPost>());
        services.AddRepositories();  

        return services;
    }

    private static IServiceCollection AddRepositories(
           this IServiceCollection services
       )
    {
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }


}