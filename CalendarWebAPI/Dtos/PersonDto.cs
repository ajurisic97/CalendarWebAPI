using CalendarWebAPI.Models;
using Newtonsoft.Json.Linq;

namespace CalendarWebAPI.Dtos
{
    public class PersonDto
    {
        public static Person FromJson(JObject json)
        {
            var jsonId = json["Id"];
            var Id = Guid.NewGuid();
            if(jsonId!=null)
                Id = Guid.Parse(jsonId.ToString());
            var FirstName = json["firstName"].ToObject<string>();
            var LastName = json["lastName"].ToObject<string>();
            var Address = json["address"].ToObject<string>();
            var PostalCode = json["postalCode"].ToObject<string>();
            var City = json["city"].ToObject<string>();
            var Country = json["country"].ToObject<string>();
            var CountryOfResidence = json["countryResidence"].ToObject<string>();
            var Gender = json["gender"].ToObject<string>();
            var DateOfBirth = json["dateOfBirth"].ToObject<DateTime>();
            var PersonalIdentificationNumber = json["pin"].ToObject<string>();
            return new Person(Id, null, FirstName, LastName, Address, PostalCode, City, Country, CountryOfResidence, DateOfBirth, Gender, PersonalIdentificationNumber, null, null);
        }
    }
}
