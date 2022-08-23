using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class CreatorMapper
    {
        public static Creator FromDatabase(DbModels.Creator creator)
        {
            return new Creator(creator.Id, creator.Name);
        }
    }
}
