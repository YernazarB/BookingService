using BookingService.Application.Responses;
using MediatR;

namespace BookingService.Application.Requests.Queries
{
    public class GetTablesQuery : BaseRequest<TableViewModel[]>
    {
    }
}
