using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class PersonMapper
    {
        public static Person FromDatabase(DbModels.Person person)
        {
            return new Person(person.Id, person.Code, person.FirstName, person.LastName, person.Adress, person.PostalCode, person.City, person.Country, person.CountryOfResidence,
                person.DateOfBirth, person.Gender, person.PersonalIdentificationNumber, person.ImageUrl, person.Description);
        }

        public static DbModels.Person ToDatabase(Person person)
        {
            return new DbModels.Person
            {
                Id = person.Id.HasValue ? person.Id.Value : new Guid(),
                Code = person.Code,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Adress = person.Adress,
                PostalCode = person.PostalCode,
                City = person.City,
                Country = person.Country,
                CountryOfResidence = person.CountryOfResidence,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                PersonalIdentificationNumber = person.PersonalIdentificationNumber,
                ImageUrl = person.ImageUrl,
                Description = person.Description,


            };
        }
    }
}
