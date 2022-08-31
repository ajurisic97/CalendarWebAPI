using CalendarWebAPI.DbModels;
using System.Text.Json;

namespace CalendarWebAPI.Repositories
{
    public class EventRepository
    {
        private readonly CalendarContext _dbContext;
        public EventRepository(CalendarContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void AddRecurrings()
        {
            List<Models.Recurring> listRecurrings = new List<Models.Recurring>();
            using(StreamReader r = new StreamReader("Recurring.json"))
            {
                var recurrings = r.ReadToEnd();
                listRecurrings = JsonSerializer.Deserialize<List<Models.Recurring>>(recurrings);
            }
            
        }
        public void AddEvents()
        {
            
        }

        public void AddPerson()
        {
            //adding Scheduler after adding Person (And for that we first need Recurring and then Event)
        }
    }
}
