using BookingService.Application.Interfaces;
using BookingService.Application.Requests.Commands;
using BookingService.Application.Responses;
using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.Handlers.CommandHandlers
{
    public class DeleteGroupCommandHandler : BaseHandler<DeleteGroupCommand, object>
    {
        public DeleteGroupCommandHandler(IAppDbContext dbContext, ILogger<DeleteGroupCommandHandler> logger) : base(dbContext, logger)
        {
        }

        protected override async Task<BaseResponse<object>> HandleRequest(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await DbContext.Groups.Where(x => x.Id == request.Id).Include(x => x.Table).FirstOrDefaultAsync();
            if (group == null)
            {
                return NotFound(nameof(group));
            }

            //Release the table
            var table = group.Table;
            if (table != null)
            {
                table.Occupied -= group.Size;
                table.Remainder += group.Size;
            }

            //Remove the group
            DbContext.Groups.Remove(group);
            await DbContext.SaveChangesAsync();

            //Add pending groups to the table
            if (table != null)
            {
                var pendingGroup = await GetPendingGroup(table);
                while (pendingGroup != null)
                {
                    table.Occupied += pendingGroup.Size;
                    table.Remainder -= pendingGroup.Size;
                    pendingGroup.TableId = table.Id;

                    await DbContext.SaveChangesAsync();
                    pendingGroup = await GetPendingGroup(table);
                }
            }

            return new BaseResponse<object>();
        }

        private async Task<Group> GetPendingGroup(Table table)
        {
            var group = await DbContext.Groups
                .Where(x => !x.TableId.HasValue && x.Size <= table.Remainder)
                .OrderBy(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            return group;
        }
    }
}
