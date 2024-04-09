using Microsoft.OpenApi.Models;
using System.Security.Principal;

namespace AnimesProtech.Application
{
    public static class DependencyInjectionSwagger
    {
        public static IServiceCollection AddConfigSwagger(this IServiceCollection services)
        { 
            services.AddSwaggerGen(it => 
            { 
                it.SwaggerDoc("v1", new OpenApiInfo { Title = "API ANIME PROTECH", Version = "v1" });

                it.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                it.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }
    }
}
