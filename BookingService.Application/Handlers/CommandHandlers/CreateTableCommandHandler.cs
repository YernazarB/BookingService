using BookingService.Application.Interfaces;
using BookingService.Application.Managers;
using BookingService.Application.Requests.Commands;
using BookingService.Application.Responses;
using BookingService.Domain.Entities;
using BookingService.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.Handlers.CommandHandlers
{
    public class CreateTableCommandHandler : BaseHandler<CreateTableCommand, TableViewModel>
    {
        private readonly IRestManager _restManager;
        public CreateTableCommandHandler(IAppDbContext dbContext, ILogger<CreateTableCommandHandler> logger, IRestManager restManager) : base(dbContext, logger)
        {
            _restManager = restManager;
        }

        protected override async Task<BaseResponse<TableViewModel>> HandleRequest(CreateTableCommand request, CancellationToken cancellationToken)
        {
            var newTable = new Table { Size = request.Size, Remainder = request.Size };
            await DbContext.Tables.AddAsync(newTable);
            await DbContext.SaveChangesAsync(cancellationToken);
            await _restManager.ReserveTable(newTable.Id);

            var model = new TableViewModel { Id = newTable.Id, Size = newTable.Size };
            return new BaseResponse<TableViewModel>(model, ResponseCode.Created);
        }
    }
}
