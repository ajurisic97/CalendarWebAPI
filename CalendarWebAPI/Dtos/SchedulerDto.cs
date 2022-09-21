using CalendarWebAPI.Models;
using Newtonsoft.Json.Linq;

namespace CalendarWebAPI.Dtos
{
    public class SchedulerDto
    {
        //PUT
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

        //POST
        public static SchedulerItem SIFromJson(JObject json)
        {
            var startTime = json["StartTime"].ToObject<TimeSpan>();
            var endTime = json["EndTime"].ToObject<TimeSpan>();
            var date = json["Date"].ToObject<DateTime>();
            var schedulerItem = new SchedulerItem(null, startTime, endTime, date);
            return schedulerItem;
        }

        public static SchedulerItem FromJsonEdit(JObject json)
        {
            var id = json["Id"].ToObject<Guid>();
            var startTime = json["StartTime"].ToObject<TimeSpan>();
            var endTime = json["EndTime"].ToObject<TimeSpan>();
            var date = json["Date"].ToObject<DateTime>();
            var schedulerItem = new SchedulerItem(id, startTime, endTime, date);
            return schedulerItem;
        }
    }
}
