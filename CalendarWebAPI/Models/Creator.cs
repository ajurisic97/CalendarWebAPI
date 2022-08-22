namespace CalendarWebAPI.Models
{
    public class Creator
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public Creator(Guid? id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
