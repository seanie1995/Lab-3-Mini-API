using Lab_3_Mini_API.Data;
using Lab_3_Mini_API.Models.DTO;
using Lab_3_Mini_API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Lab_3_Mini_API.Handlers
{
    public class UrlHandler
    {
        public static IResult ListPersonLink(ApplicationContext context, string lastName)
        {
            UrlViewModels[] result =
                context.InterestUrls
                .Include(i => i.Interests.Persons)
                .Where(i => i.Persons.LastName == lastName)
                .Select(x => new UrlViewModels()
                {
                    Url = x.Url,
                }).ToArray();
            return Results.Json(result);
        }

        public static IResult AddNewLink(ApplicationContext context, string lastName, string interestName, UrlDto url)
        {
            var person = context.Persons
                    .Include(p => p.Interests)
                    .FirstOrDefault(p => p.LastName == lastName);

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

            if (string.IsNullOrEmpty(url.Url))
            {
                return Results.BadRequest(new { Message = "No link was provided" });
            }

            context.InterestUrls.Add(new Models.InterestUrl()
            {
                Url = url.Url,
                Persons = person,
                Interests = interest
            });

            context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        public static IResult ListAllLinks(ApplicationContext context)
        {
            UrlViewModels[] result = 
                 context.InterestUrls
                .Select(p => new UrlViewModels
                {
                    Url = p.Url,
                }).ToArray();

            return Results.Json(result);
            
        }
    }
}
