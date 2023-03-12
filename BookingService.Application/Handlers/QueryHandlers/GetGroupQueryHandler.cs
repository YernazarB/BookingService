using BookingService.Application.Interfaces;
using BookingService.Application.Requests.Queries;
using BookingService.Application.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.Handlers.QueryHandlers
{
    public class GetGroupQueryHandler : BaseHandler<GetGroupQuery, GroupViewModel>
    {
        public GetGroupQueryHandler(IAppDbContext dbContext, ILogger<GetGroupQueryHandler> logger) : base(dbContext, logger)
        {
        }

        protected override async Task<BaseResponse<GroupViewModel>> HandleRequest(GetGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await DbContext.Groups.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (group == null) 
            {
                return NotFound(nameof(group));
            }

            return new BaseResponse<GroupViewModel>(
                new GroupViewModel 
                { 
                    Id = group.Id, 
                    Size = group.Size,
                    TableId = group.TableId
                });
        }
    }
}
