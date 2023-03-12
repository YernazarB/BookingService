namespace BookingService.Application.Requests.Commands
{
    public class DeleteTableCommand : BaseRequest<object>
    {
        public int Id { get; set; }
    }
}
