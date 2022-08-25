namespace CalendarWebAPI.Models
{
    public class FullSchedulerItem
    {
        public List<SchedulerItem> SchedulersItems { get; set; }

        public string Name;

        public decimal Coef;


        //public FullSchedulerItem(Guid? schedulerId, DateTime? date, bool? isWorkingDay)
        //{
        //    SchedulerId = schedulerId;
        //    Date = date;
        //    this.isWorkingDay = isWorkingDay;
        //}
    }
}
