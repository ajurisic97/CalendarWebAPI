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
                var date = json["start"].ToObject<DateTime>();
                var startTime = new TimeSpan(date.Hour, date.Minute, date.Second);
                
                var end = json["end"].ToObject<DateTime>();

                var endTime = new TimeSpan(end.Hour, end.Minute, end.Second);

                var personId = json["personId"].ToObject<Guid>();
                var eventType = json["title"].ToObject<int>();
                var endDate = json["endDate"].ToObject<DateTime>();
                var createdByUser = json["createdByUser"].ToObject<bool>();
                var schedulerItemsId=Guid.NewGuid();
                if (json.ContainsKey("eventId"))
                {
                    schedulerItemsId = json["eventId"].ToObject<Guid>();
                }
                var description = json["description"].ToObject<string>();
                var schedulerItem = new SchedulerItem(schedulerItemsId, startTime, endTime, date,description,createdByUser);
                var typeOfRecurring = json["Recurring"].ToObject<string>();
                var scheduler = new RecurringSchedulerItems(personId, eventType, schedulerItem, typeOfRecurring, endDate);
                items.Add(scheduler);
            }
            
            return items;
        }
    }
}
