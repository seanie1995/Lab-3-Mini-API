using Lab_3_Mini_API.Data;
using Lab_3_Mini_API.Models;
using Microsoft.EntityFrameworkCore;

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

            app.MapGet("/AllPersons", (ApplicationContext context) =>
            {
                return Results.Json(context.Persons.Select(p => new {p.LastName, p.FirstName, p.PhoneNumber}).ToArray());
            });

            app.MapGet("/AllInterests", (ApplicationContext context) =>
            {
                return Results.Json(context.Interests.Select(p => new {p.InterestName, p.InterestDescription}).ToArray());
            });

            app.MapGet("/{LastName}", (ApplicationContext context, string lastName) => 
            {
                Persons? person = context.Persons.Where(p => p.LastName == lastName).SingleOrDefault();

                return Results.Json(person);
            });

            app.MapGet("/{lastName}/interests", (ApplicationContext context, string lastName) =>
            {
                
            });
         
            app.Run();
        }
    }
}
