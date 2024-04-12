using AnimesProtech.Application.UseCases;
using AnimesProtech.Domain.Interfaces.DbContext;
using AnimesProtech.Domain.Interfaces.Repositorys;
using AnimesProtech.Domain.Interfaces.UseCases;
using AnimesProtech.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnimesProtech.ProjectExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IAppDbContext, AppDbContext>();
            services.AddTransient<IAnimeRepository, AnimeRepository>();
            services.AddTransient<IAddAnimeUseCase, AddAnimeUseCase>();
            services.AddTransient<IGetAnimesUseCase, GetAnimesUseCase>();
            services.AddTransient<IUpdateAnimeUseCase, UpdateAnimeUseCase>();
            services.AddTransient<IDeleteAnimeUseCase, DeleteAnimeUseCase>();

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                )
            );

            return services;
        }

        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }

        public static IServiceCollection AddLoggingServices(this IServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.AddConsole();
            });

            return services;
        }
    }
}
