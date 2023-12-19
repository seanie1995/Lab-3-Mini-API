using Lab_3_Mini_API.Data;
using Lab_3_Mini_API.Models;
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
             
            app.MapGet("/AllPersons", (ApplicationContext context) =>
            {
                return Results.Json(context.Persons.Select(p => new {p.LastName, p.FirstName }).ToArray());
            });

            app.MapGet("/AllLinks", (ApplicationContext context) =>
            {
                return Results.Json(context.InterestUrls.ToArray());
            });

            //app.MapGet("/AllLinks", (ApplicationContext context) =>
            //{
            //    return Results.Json(context.InterestUrls.Select(p => new { p.Id, p.Url }).ToArray());
            //});

            app.MapGet("/AllInterests", (ApplicationContext context) =>
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

            app.MapGet("/{lastName}/interests", (ApplicationContext context, string lastName) =>
            {
                var interest = context.Persons
                        .Include(i => i.Interests)
                        .Where(i => i.LastName == lastName)
                        .Select(i => new { i.FirstName, i.LastName, Interests = i.Interests.Select(i => new {i.Name, i.Description})})
                        .ToArray();

                if (interest == null)
                {
                    return Results.NotFound();
                }

                return Results.Json(interest);

            });

            app.MapGet("/{lastName}/links", (ApplicationContext context, string lastName) =>
            {
                var links = context.InterestUrls
                        .Include(i => i.Interests)
                            .ThenInclude(i => i.Persons)
                        .Where(i => i.Interests.Persons.Any(i => i.LastName == lastName))
                        .Select(i => i.Url)
                        .ToArray();
                      
                if (links == null)
                {
                    return Results.NotFound();
                }

                return Results.Json(links);

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

            app.MapPost("/{lastName}/{interestName}/addUrl", (ApplicationContext context, string lastName, string interestName, InterestUrl newUrl   ) =>
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
