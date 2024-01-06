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
                    phoneNumber = x.PhoneNumber,
                }).ToArray();
            return Results.Json(result);

            //return Results.Json(context.Persons.Select(p => new { p.Id, p.FirstName, p.LastName, p.phoneNumber, p.Interests.Count }).ToArray());
        }

        public static IResult AddNewInterest(ApplicationContext context, int id, InterestDto newInterest)
        {
            var person = context.Persons
                    .Include(p => p.Interests)
                    .FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return Results.NotFound();
            }

            var allInterests = context.Interests        
                .ToArray();

            if (allInterests.Any(i => i.Name == newInterest.Name))
            {
                return Results.Conflict("Interest already exists in the database");
            }

            if (string.IsNullOrEmpty(newInterest.Name) || string.IsNullOrEmpty(newInterest.Description))
            {
                return Results.BadRequest(new { Message = "New interest needs to have both title and content" });
            }
            var interest = new Interests
            {
                Name = newInterest.Name,
                Description = newInterest.Description
            };
                     
            person.Interests.Add(interest);
            

            context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        public static IResult AddExistingInterest(ApplicationContext context, int personId, int interestId)
        {
            var person = context.Persons
                .Where(p => p.Id == personId)
                .Include(p => p.Interests)
                .SingleOrDefault();

            if (person == null)
            {
                return Results.Conflict("Person not found");
            }

            var interest = context.Interests
                .Where(i => i.Id == interestId)
                .Include(p => p.Persons)
                .SingleOrDefault();

            if (interest == null)
            {
                return Results.Conflict("Interest not found");
            }

            person.Interests.Add(interest);

            context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
