using BookingService.Application.Interfaces;
using BookingService.Application.Managers;
using BookingService.Application.Requests.Commands;
using BookingService.Application.Responses;
using BookingService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.Handlers.CommandHandlers
{
    public class CreateGroupCommandHandler : BaseHandler<CreateGroupCommand, GroupViewModel>
    {
        private readonly IRestManager _restManager;
        public CreateGroupCommandHandler(IAppDbContext dbContext, ILogger<CreateGroupCommandHandler> logger, IRestManager restManager) : base(dbContext, logger)
        {
            _restManager = restManager;
        }

        protected override async Task<BaseResponse<GroupViewModel>> HandleRequest(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var newGroup = new Group
            {
                Size = request.Size,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await _restManager.OnArrive(newGroup);

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
