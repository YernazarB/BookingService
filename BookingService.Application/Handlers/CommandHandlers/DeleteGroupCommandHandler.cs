using BookingService.Application.Interfaces;
using BookingService.Application.Managers;
using BookingService.Application.Requests.Commands;
using BookingService.Application.Responses;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.Handlers.CommandHandlers
{
    public class DeleteGroupCommandHandler : BaseHandler<DeleteGroupCommand, object>
    {
        private readonly IRestManager _restManager;
        public DeleteGroupCommandHandler(IAppDbContext dbContext, ILogger<DeleteGroupCommandHandler> logger, IRestManager restManager) : base(dbContext, logger)
        {
            _restManager = restManager;
        }

        protected override async Task<BaseResponse<object>> HandleRequest(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var tableId = await _restManager.OnLeave(request.Id);
            if (tableId.HasValue)
            {
                await _restManager.ReserveTable(tableId.Value);
            }

            return new BaseResponse<object>();
        }
    }
}
