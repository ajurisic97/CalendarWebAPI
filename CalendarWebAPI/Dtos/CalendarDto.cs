using CalendarWebAPI.Models;
using Newtonsoft.Json.Linq;

namespace CalendarWebAPI.Dtos
{
    public class CalendarDto
    {
        public static Calendar FromJson(JObject json)
        {
            var id = json["Id"].ToObject<Guid?>();
            var paentId=json["PaentId"].ToObject<Guid?>();
            var parent = new Calendar(paentId, null, null, 0, null, null, null, null, null);
            var creatorId = json["CreatorId"].ToObject<Guid?>();
            var creator = new Creator(creatorId, "");
            var year = json["Year"].ToObject<int>();
            var description = json["Description"].ToObject<string>();
            var startDate = json["StartDate"].ToObject<DateTime?>();
            var endDate = json["EndDate"].ToObject<DateTime?>();
            var createdDate = json["CreatedDate"].ToObject<DateTime?>();
            var isApproved = json["IsApproved"].ToObject<bool?>();
            return new Calendar(id, parent, creator, year, description, startDate, endDate, createdDate, isApproved);
        }
    }
}

