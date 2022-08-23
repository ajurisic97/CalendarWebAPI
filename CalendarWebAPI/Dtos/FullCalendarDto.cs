using CalendarWebAPI.Models;

namespace CalendarWebAPI.Dtos
{
    public class FullCalendarDto
    {
        public Guid? CalendarId { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<FullCalendarDto> SubCalendars { get; set; }
        public List<CalendarItem> CalendarItems { get; set; }
    }
}
