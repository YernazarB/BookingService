using BookingService.Application.Responses;

namespace BookingService.Application.Requests.Queries
{
    public class GetGroupQuery : BaseRequest<GroupViewModel>
    {
        public int Id { get; set; }
    }
}
