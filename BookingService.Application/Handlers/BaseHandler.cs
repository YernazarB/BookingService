using BookingService.Application.Interfaces;
using BookingService.Application.Responses;
using BookingService.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.Handlers
{
    public abstract class BaseHandler<T, E> : IRequestHandler<T, BaseResponse<E>> where T : IRequest<BaseResponse<E>> where E : class
    {
        protected readonly IAppDbContext DbContext;
        protected readonly ILogger Logger;

        public BaseHandler(IAppDbContext dbContext, ILogger logger)
        {
            DbContext = dbContext;
            Logger = logger;
        }

        public Task<BaseResponse<E>> Handle(T request, CancellationToken cancellationToken)
        {
            try
            {
                return HandleRequest(request, cancellationToken);
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                Logger.LogError(message);
                return Task.FromResult(BadRequest(message));
            }
        }

        protected abstract Task<BaseResponse<E>> HandleRequest(T request, CancellationToken cancellationToken);

        protected BaseResponse<E> BadRequest(string message)
        {
            return new BaseResponse<E>(code: ResponseCode.BadRequest, errorMessage: message);
        }

        protected BaseResponse<E> NotFound(string objectName)
        {
            return new BaseResponse<E>(code: ResponseCode.NotFound, errorMessage: $"{objectName} not found.");
        }
    }
}
