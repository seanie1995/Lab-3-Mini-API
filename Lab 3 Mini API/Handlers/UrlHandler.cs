using Lab_3_Mini_API.Data;
using Lab_3_Mini_API.Models;
using Lab_3_Mini_API.Models.DTO;
using Lab_3_Mini_API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Lab_3_Mini_API.Handlers
{
    public class UrlHandler
    {
        public static IResult ListPersonLink(ApplicationContext context, int id)
        {
            UrlViewModels[] result =
                context.InterestUrls
                .Include(i => i.Interests.Persons)
                .Where(i => i.Persons.Id == id)
                .Select(x => new UrlViewModels()
                {
                    Url = x.Url,
                }).ToArray();
            return Results.Json(result);
        }

        public static IResult AddNewLink(ApplicationContext context, int personId, int interestId, UrlDto url)
        {
            var person = context.Persons                  
                    .Include(p => p.Interests)
                    .FirstOrDefault(p => p.Id == personId);

            if (person == null && person.Interests.Any(x => x.Id != interestId))
            {
                return Results.NotFound();
            }

            var interest = context.Interests                  
                    .Include(p => p.InterestUrls)
                    .Where(p => p.Id == interestId)
                    .FirstOrDefault();

            if (interest == null)
            {
                return Results.NotFound();
            }

            if (string.IsNullOrEmpty(url.Url))
            {
                return Results.BadRequest(new { Message = "No link was provided" });
            }

            var newLink = new InterestUrl
            {
                Url = url.Url,
                Persons = person,
                Interests = interest
            };
          
            interest.InterestUrls.Add(newLink);
                             
            context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        public static IResult ListAllLinks(ApplicationContext context)
        {
            UrlViewModels[] result = 
                 context.InterestUrls
                .Select(p => new UrlViewModels
                {
                    Id = p.Id,
                    Url = p.Url,
                }).ToArray();

            return Results.Json(result);
            
        }
    }
}
