﻿using Microsoft.OpenApi.Models;
using System.Reflection;

namespace TodoWebApp_Server_v2.Configurations
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerSetup( this IServiceCollection services )
        {
            services.AddSwaggerGen(
                x =>
                {
                    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger TodoWebAppAPI", Version = "v1" });

                    //Config Custom xml
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var filePath = Path.Combine(System.AppContext.BaseDirectory, xmlFile);
                    x.IncludeXmlComments( filePath );

                    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header using the Bearer schema. \r\n\r\n
                                    Enter 'Bearer' [space] and then your token in the text input below.
                                \r\n\r\nExample: 'Bearer 12345abcef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                    x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
                });

            return services;
        }
    }
}
