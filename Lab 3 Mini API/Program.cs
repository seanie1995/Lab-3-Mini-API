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

          
            app.MapGet("/links", (ApplicationContext context) =>
            {
                return Results.Json(context.InterestUrls.ToArray());
            });        
            app.MapGet("/interests", (ApplicationContext context) =>
            {
                var interests = context.Interests
                        .Include(p => p.InterestUrls)
                        .Select(p => new { p.Id, p.Name, p.Description, InterestUrls = p.InterestUrls.Select(p => new { p.Id, p.Url }) })
                        .ToArray();

                if (interests == null)
                {
                    return Results.NotFound();
                }
                
                return Results.Json(interests);
                        
            });
         
                            
            app.MapPost("/{lastName}/interests/{id}", (ApplicationContext context, string lastName, int id) =>
            {              
                var person = context.Persons
                    .Include(p => p.Interests) 
                    .SingleOrDefault(p => p.LastName == lastName);

                if (person == null)
                {
                    return Results.NotFound();
                }

                var interest = context.Interests
                        .Where(p => p.Id == id)
                        .SingleOrDefault();

                if (interest == null)
                {
                    return Results.NotFound();
                }

                if (person.Interests.Any(i => i.Id == id))
                {
                    return Results.Conflict("Interest already exists");
                }

                person.Interests.Add(interest);

                context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);
            });

            app.MapPost("/{lastName}/{interestName}/newUrl", (ApplicationContext context, string lastName, string interestName, InterestUrl newUrl   ) =>
            {
                var person = context.Persons
                    .Include(p => p.Interests)
                    .SingleOrDefault(p => p.LastName == lastName);

                if (person == null)
                {
                    return Results.NotFound();
                }

                var interest = context.Interests
                        .Include(p => p.InterestUrls)
                        .Where(p => p.Name == interestName)
                        .SingleOrDefault();

                if (interest == null)
                {
                    return Results.NotFound();
                }

                newUrl.Interests = interest;
                newUrl.Persons = person;

                interest.InterestUrls.Add(newUrl);
                context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);

            });

            app.Run();
        }
    }
}
