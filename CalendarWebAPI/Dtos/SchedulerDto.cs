﻿using CalendarWebAPI.Models;
using Newtonsoft.Json.Linq;

namespace CalendarWebAPI.Dtos
{
    public class SchedulerDto
    {
        public static SchedulerInfo FromJson(JObject json)
        {
            var Id = json["Id"].ToObject<Guid>();
            var schedulerId = json["SchedulerId"].ToObject<Guid>();
            var startTime = json["StartTime"].ToObject<TimeSpan>();
            var endTime = json["EndTime"].ToObject<TimeSpan>();
            var date = json["Date"].ToObject<DateTime>();
            var schedulerItem = new SchedulerItem(Id,startTime, endTime, date);
            return new SchedulerInfo(schedulerItem, schedulerId);


        }
    }
}
