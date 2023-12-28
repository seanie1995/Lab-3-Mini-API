using Lab_3_Mini_API.Data;
using Lab_3_Mini_API.Models.ViewModels;

namespace Lab_3_Mini_API.Handlers
{
    public class PersonHandler
    {
        public static IResult ListAllPersons(ApplicationContext context)
        {
            PersonViewModel[] result =
                context.Persons
                .Select(x => new PersonViewModel()
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                }).ToArray();
            return Results.Json(result);

            //return Results.Json(context.Persons.Select(p => new { p.Id, p.FirstName, p.LastName, p.phoneNumber, p.Interests.Count }).ToArray());
        }
    }
}
