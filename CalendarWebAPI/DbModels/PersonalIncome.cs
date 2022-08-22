using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class PersonalIncome
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public bool? IsProtected { get; set; }
        public bool? IsActive { get; set; }
    }
}
