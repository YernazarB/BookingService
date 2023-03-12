namespace BookingService.Domain.Entities
{
    public class Table : BaseEntity
    {
        public int Size { get; set; }
        public int Occupied { get; set; }
        public int Remainder { get; set; }
    }
}
