using BookingService.Application.Interfaces;
using BookingService.Application.Requests.Commands;
using BookingService.Application.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.Handlers.CommandHandlers
{
    public class DeleteTableCommandHandler : BaseHandler<DeleteTableCommand, object>
    {
        public DeleteTableCommandHandler(IAppDbContext dbContext, ILogger<DeleteTableCommandHandler> logger) : base(dbContext, logger)
        {
        }

        protected override async Task<BaseResponse<object>> HandleRequest(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            var table = await DbContext.Tables.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (table == null)
            {
                return NotFound(nameof(table));
            }

            DbContext.Tables.Remove(table);
            await DbContext.SaveChangesAsync(cancellationToken);
            return new BaseResponse<object>();
        }
    }
}
