using BookingService.Application.Requests.Commands;
using BookingService.Application.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : BaseController
    {
        public GroupsController(ISender mediatR) : base(mediatR)
        {
        }

        [HttpGet]
        [Route("lookup")]
        public async Task<IActionResult> Lookup(int groupId)
        {
            var response = await MediatR.Send(new GetGroupQuery { Id = groupId });
            return CreateResponse(response);
        }

        [HttpPost]
        [Route("onarrive")]
        public async Task<IActionResult> OnArrive(CreateGroupCommand command)
        {
            var response = await MediatR.Send(command);
            return CreateResponse(response);
        }

        [HttpDelete]
        [Route("onleave")]
        public async Task<IActionResult> OnLeave(DeleteGroupCommand command)
        {
            var response = await MediatR.Send(command);
            return CreateResponse(response);
        }
    }
}
