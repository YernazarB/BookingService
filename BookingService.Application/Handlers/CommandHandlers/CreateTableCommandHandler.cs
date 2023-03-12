using BookingService.Application.Interfaces;
using BookingService.Application.Requests.Commands;
using BookingService.Application.Responses;
using BookingService.Domain.Entities;
using BookingService.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.Handlers.CommandHandlers
{
    public class CreateTableCommandHandler : BaseHandler<CreateTableCommand, TableViewModel>
    {
        public CreateTableCommandHandler(IAppDbContext dbContext, ILogger<CreateTableCommandHandler> logger) : base(dbContext, logger)
        {
        }

        protected override async Task<BaseResponse<TableViewModel>> HandleRequest(CreateTableCommand request, CancellationToken cancellationToken)
        {
            var newTable = new Table { Size = request.Size, Remainder = request.Size };
            await DbContext.Tables.AddAsync(newTable);
            await DbContext.SaveChangesAsync(cancellationToken);

            var model = new TableViewModel { Id = newTable.Id, Size = newTable.Size };
            return new BaseResponse<TableViewModel>(model, ResponseCode.Created);
        }
    }
}
