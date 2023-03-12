using BookingService.Application.Responses;
using MediatR;

namespace BookingService.Application.Requests
{
    public abstract class BaseRequest<T> : IRequest<BaseResponse<T>> where T : class
    {
    }
}
