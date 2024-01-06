using Lab_3_Mini_API.Data;
using Lab_3_Mini_API.Handlers;
using Lab_3_Mini_API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Lab_3_Mini_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
             
            app.MapGet("/persons", PersonHandler.ListAllPersons);
            app.MapGet("/{lastName}/links", UrlHandler.ListPersonLink);
            app.MapGet("/{lastName}/interests", InterestHandler.ListPersonInterests);

            app.MapGet("/links", UrlHandler.ListAllLinks);
            app.MapGet("/interests", InterestHandler.ListAllInterests);
            
            app.MapPost("/{lastName}/interests", PersonHandler.AddNewInterest);
            app.MapPost("/{lastName}/{interestName}/links", UrlHandler.AddNewLink);
                                                       
            app.Run();
        }
    }
}
