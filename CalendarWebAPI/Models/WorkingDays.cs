namespace CalendarWebAPI.Models
{
    public class WorkingDays
    {
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public WorkingDays(bool? monday, bool? tuseday, bool? wednesday, bool? thursday, bool? friday, bool saturday, bool sunday)
        {
            Monday = (bool)monday;
            Tuesday = (bool)tuseday;
            Wednesday = (bool)wednesday;
            Thursday = (bool)thursday;
            Friday = (bool)friday;
            Saturday = saturday;
            Sunday = sunday;
        }
    }
}
