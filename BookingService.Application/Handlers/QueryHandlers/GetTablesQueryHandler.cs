using BookingService.Application.Interfaces;
using BookingService.Application.Requests.Queries;
using BookingService.Application.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.Handlers.QueryHandlers
{
    public class GetTablesQueryHandler : BaseHandler<GetTablesQuery, TableViewModel[]>
    {
        public GetTablesQueryHandler(IAppDbContext dbContext, ILogger<GetTablesQueryHandler> logger) : base(dbContext, logger)
        {
        }

        protected override async Task<BaseResponse<TableViewModel[]>> HandleRequest(GetTablesQuery request, CancellationToken cancellationToken)
        {
            var tables = await DbContext.Tables
                .Select(x => new TableViewModel
                {
                    Id = x.Id,
                    Size = x.Size
                }).AsNoTracking().ToArrayAsync();

            return new BaseResponse<TableViewModel[]>(tables);
        }
    }
}
