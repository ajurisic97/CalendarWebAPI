using System.ComponentModel.DataAnnotations;

namespace CalendarWebAPI.Models
{
    public class Person
    {
        public Guid? Id { get; set; }

        public string? Code { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Adress { get; set; }

        public string? PostalCode { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? CountryOfResidence { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string PersonalIdentificationNumber { get; set; }
        public DateTime? RecordDtModified { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public string? ImageUrl { get; set; }

        public string? Description { get; set; }

        public Person(Guid? id, string? code, string firstName, string lastName, string? adress, string? postalCode, string? city, string? country, string? countryOfResidence, DateTime? dateOfBirth, string gender, string personalIdentificationNumber, DateTime? recordDtModified, byte[]? rowVersion, string? imageUrl, string? description)
        {
            Id = id;
            Code = code;
            FirstName = firstName;
            LastName = lastName;
            Adress = adress;
            PostalCode = postalCode;
            City = city;
            Country = country;
            CountryOfResidence = countryOfResidence;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            PersonalIdentificationNumber = personalIdentificationNumber;
            RecordDtModified = recordDtModified;
            RowVersion = rowVersion;
            ImageUrl = imageUrl;
            Description = description;
        }


    }
}
