namespace CalendarWebAPI.DbModels
{
    public partial class ApplicationEvent
    {
        public Guid ApplicationId { get; set; }
        public Guid EventId { get; set; }
        public virtual Application Application { get; set; } = null!;
        public virtual Event Event { get; set; } = null!;
    }
}
