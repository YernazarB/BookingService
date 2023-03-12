namespace BookingService.Domain.Entities
{
    public class Group : BaseEntity
    {
        public int Size { get; set; }
        public int? TableId { get; set; }
        public Table Table { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
