using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MVCMoviesAPI.Models;
using System.Configuration;
using System.Reflection;

namespace MVCMoviesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<MvcMovieContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovieContext")));
           

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Movies API",
                    Description = "An ASP.NET Core Web API for managing Movie items",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.Use((context, next) =>
            {
                if (context.Request.Headers.ContainsKey("Authorization"))
                {
                    string authToken = context.Request.Headers["Authorization"];
                        System.Diagnostics.Trace.WriteLine(authToken);
                }
                return next();
            });


            app.MapControllers();

            app.Run();
        }
    }
}