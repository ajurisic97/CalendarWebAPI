namespace CalendarWebAPI.Models
{
    public class FullSchedulerItem
    {
        public List<SchedulerItem> SchedulersItems { get; set; }

        public string Name;

        public decimal Coef;
    }
}
