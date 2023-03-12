using BookingService.Application.Interfaces;
using BookingService.Application.Requests.Commands;
using BookingService.Application.Responses;
using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.Handlers.CommandHandlers
{
    public class CreateGroupCommandHandler : BaseHandler<CreateGroupCommand, GroupViewModel>
    {
        public CreateGroupCommandHandler(IAppDbContext dbContext, ILogger<CreateGroupCommandHandler> logger) : base(dbContext, logger)
        {
        }

        protected override async Task<BaseResponse<GroupViewModel>> HandleRequest(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var newGroup = new Group
            {
                Size = request.Size,
                CreatedDate = DateTimeOffset.UtcNow
            };

            //Seek an empty table
            var table = await DbContext.Tables.Where(x => x.Remainder == x.Size && x.Size >= newGroup.Size).OrderBy(x => x.Size).FirstOrDefaultAsync();

            //If there is no empty table, seek another available table
            if (table == null)
            {
                table = await DbContext.Tables.FirstOrDefaultAsync(x => x.Remainder >= newGroup.Size);
            }

            //Book the table if it is not null
            if (table != null)
            {
                newGroup.TableId = table.Id;
                table.Occupied += newGroup.Size;
                table.Remainder -= newGroup.Size;
            }

            await DbContext.Groups.AddAsync(newGroup);
            await DbContext.SaveChangesAsync();

            var model = new GroupViewModel
            {
                Id = newGroup.Id,
                Size = newGroup.Size,
                TableId = newGroup.TableId
            };
            return new BaseResponse<GroupViewModel>(model);
        }
    }
}
