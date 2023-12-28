using Lab_3_Mini_API.Data;
using Lab_3_Mini_API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

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
    }
}
