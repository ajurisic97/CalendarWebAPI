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
            var description = json["Description"].ToObject<string>();
            var createdByUser = json["CreatedByUser"].ToObject<bool>();
            var schedulerItem = new SchedulerItem(Id,startTime, endTime, date,description,createdByUser);
            return new SchedulerInfo(schedulerItem, schedulerId);
        }

        //POST
        public static SchedulerItem SIFromJson(JObject json)
        {
            var startTime = json["StartTime"].ToObject<TimeSpan>();
            var endTime = json["EndTime"].ToObject<TimeSpan>();
            var date = json["Date"].ToObject<DateTime>();
            var description = json["Description"].ToObject<string>();
            var createdByUser = json["CreatedByUser"].ToObject<bool>();

            var schedulerItem = new SchedulerItem(null, startTime, endTime, date,description,createdByUser);
            return schedulerItem;
        }

        public static SchedulerItem FromJsonEdit(JObject json)
        {
            var id = json["Id"].ToObject<Guid>();
            var startTime = json["StartTime"].ToObject<TimeSpan>();
            var endTime = json["EndTime"].ToObject<TimeSpan>();
            var date = json["Date"].ToObject<DateTime>();
            var description = json["Description"].ToObject<string>();
            var createdByUser = json["CreatedByUser"].ToObject<bool>();

            var schedulerItem = new SchedulerItem(id, startTime, endTime, date, description, createdByUser);
            return schedulerItem;
        }
    }
}
