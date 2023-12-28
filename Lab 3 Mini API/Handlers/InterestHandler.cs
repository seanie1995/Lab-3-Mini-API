using Lab_3_Mini_API.Data;
using Lab_3_Mini_API.Models;
using Lab_3_Mini_API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Lab_3_Mini_API.Handlers
{
    public class InterestHandler
    {
        public static IResult ListPersonInterests(ApplicationContext context, string lastName)
        {
            InterestsViewModel[] result =
                context.Interests
                .Include(x => x.Persons)
                .Where(x => x.Persons.Any(x => x.LastName == lastName))
                .Select(x => new InterestsViewModel()
                {
                    name = x.Name,
                    description = x.Description,
                }).ToArray();

            if (result == null)
            {
                return Results.NotFound();
            }

            return Results.Json(result);

        }
    }
}
