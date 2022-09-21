using CalendarWebAPI.Models;
using Newtonsoft.Json.Linq;

namespace CalendarWebAPI.Dtos
{
    public class RecurringSchedulersDto
    {
        //POST
        public static List<RecurringSchedulerItems> FromJson(List<JObject> jsonList)
        {
            var items = new List<RecurringSchedulerItems>();
            for (var i=0;i<jsonList.Count; i++)
            {
                var json = jsonList[i];
                var date = json["startTime"].ToObject<DateTime>();
                var startTime = new TimeSpan(date.Hour, date.Minute, date.Second);

                var end = json["endTime"].ToObject<DateTime>();

                var endTime = new TimeSpan(end.Hour, end.Minute, end.Second);

                var personId = json["Person"].ToObject<Guid>();
                var eventType = json["title"].ToObject<int>();
                var endDate = json["EndDate"].ToObject<DateTime>();
                var schedulerItem = new SchedulerItem(null, startTime, endTime, date);
                var typeOfRecurring = json["Recurring"].ToObject<string>();
                var scheduler = new RecurringSchedulerItems(personId, eventType, schedulerItem, typeOfRecurring, endDate);
                items.Add(scheduler);
            }
            
            return items;
        }
    }
}
