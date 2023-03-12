namespace BookingService.Application.Requests.Commands
{
    public class DeleteGroupCommand : BaseRequest<object>
    {
        public int Id { get; set; }
    }
}
