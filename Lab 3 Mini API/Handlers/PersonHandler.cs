using Lab_3_Mini_API.Data;
using Lab_3_Mini_API.Models;
using Lab_3_Mini_API.Models.DTO;
using Lab_3_Mini_API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

        public static IResult AddNewInterest(ApplicationContext context, string lastName, InterestDto newInterest)
        {
            var person = context.Persons
                    .Include(p => p.Interests)
                    .FirstOrDefault(p => p.LastName == lastName);

            if (person == null)
            {
                return Results.NotFound();
            }

            if(string.IsNullOrEmpty(newInterest.Name) || string.IsNullOrEmpty(newInterest.Description))
            {
                return Results.BadRequest(new { Message = "New interest needs to have both title and content" });
            }
            context.Interests.Add(new Interests()
            {
                Name = newInterest.Name,
                Description = newInterest.Description,        
                Persons = newInterest.Persons
            }); 
                       
            context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
