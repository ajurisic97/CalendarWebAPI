using CalendarWebAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarWebAPI.Dtos
{
    public class CalendarItemsDto
    {
        public static CalendarItem FromJson(JObject json)
        {
            var Id = json["Id"].ToObject<Guid?>();
            var GivenName = json["GivenName"].ToObject<string>();
            var Date = json["Date"].ToObject<DateTime?>();
            var IsHoliday = json["IsHoliday"].ToObject<bool?>();
            var IsWeekendday = json["IsWeekenday"].ToObject<bool?>();
            var IsWorkingday = json["IsWorkingday"].ToObject<bool?>();
            var IsMemorialday = json["IsApproved"].ToObject<bool?>();
            var IsActive = json["IsActive"].ToObject<bool?>();
            var IsApproved = json["IsApproved"].ToObject<bool?>();
            return new CalendarItem(Id,null,GivenName,Date,IsHoliday,IsWeekendday,IsWorkingday,IsMemorialday,IsActive,IsApproved);
        }
        

        
    }
}
