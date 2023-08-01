using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Toro.API.Configurations;
using Toro.API.Domain;
using Toro.API.Infrastructure;
using Toro.API.Infrastructure.AppSettings;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(c =>
            c.AddPolicy("*", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

        // Add services to the container.
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();

        //Mongo Database
        var appSection = builder.Configuration.GetSection("ApplicationOptions");
        builder.Services.Configure<ApplicationOptions>(appSection);

        // Dependency Injection Bootstrapper
        builder.Services.ConfigureInfrastructure();
        builder.Services.ConfigureDomain();

        builder.AddSecurityConfiguration();
        builder.AddMediatRConfiguration();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(swagger =>
        {
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Autenticação baseada em Json Web Token (JWT).",
                Type = SecuritySchemeType.Http,
            });

            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
            });
        });
        builder.Services.AddMvc();

        builder.Services.AddMvc().AddFluentValidation();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseLocalizationConfiguration();
        app.UseCors(x => x
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}