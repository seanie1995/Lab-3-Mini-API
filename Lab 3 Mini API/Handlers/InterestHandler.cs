using Lab_3_Mini_API.Data;
using Lab_3_Mini_API.Models;
using Lab_3_Mini_API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Lab_3_Mini_API.Handlers
{
    public class InterestHandler
    {
        public static IResult ListPersonInterests(ApplicationContext context, int id)
        {
            InterestsViewModel[] result =
                context.Interests
                .Include(x => x.Persons)
                .Where(x => x.Persons.Any(x => x.Id == id))
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

        public static IResult ListAllInterests(ApplicationContext context)
        {
            InterestsViewModel[] result =
                context.Interests
                .Select(x => new InterestsViewModel()
                {
                    Id = x.Id,
                    name = x.Name,
                    description = x.Description
                }).ToArray();

            if (result == null)
            {
                return Results.NotFound();
            }

            return Results.Json(result);
        }
    }
}
