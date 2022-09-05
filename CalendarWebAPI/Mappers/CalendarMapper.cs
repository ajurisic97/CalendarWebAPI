using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class CalendarMapper
    {
        public static Calendar FromDatabase(DbModels.Calendar calendar)
        {
            return new Calendar(calendar.Id, CalendarMapper.FromDatabase(calendar.Parent), 
                CreatorMapper.FromDatabase(calendar.Creator), 
                calendar.Year, calendar.Description,
                calendar.StartDate, calendar.EndDate, calendar.CreatedDate, 
                calendar.IsApproved);
        }

        public static DbModels.Calendar ToDatabase(Calendar calendar)
        {
            return new DbModels.Calendar
            {
                Id = calendar.Id.Value,
                ParentId = calendar.Paent.Id,
                CreatorId = calendar.Creator.Id,
                Year = calendar.Year, Description = calendar.Description,
                StartDate = calendar.StartDate, EndDate = calendar.EndDate, CreatedDate = calendar.CreatedDate,
                IsApproved = calendar.IsApproved
            };
        }
    }
}
