using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class Person
    {
        public Person()
        {
            Schedulers = new HashSet<Scheduler>();
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Adress { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? CountryOfResidence { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string PersonalIdentificationNumber { get; set; } = null!;
        public DateTime? RecordDtModified { get; set; }
        public byte[]? RowVersion { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Scheduler> Schedulers { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
