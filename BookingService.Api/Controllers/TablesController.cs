using BookingService.Application.Requests.Commands;
using BookingService.Application.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : BaseController
    {
        public TablesController(ISender mediatR) : base(mediatR)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await MediatR.Send(new GetTablesQuery());
            return CreateResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTableCommand command)
        {
            var response = await MediatR.Send(command);
            return CreateResponse(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteTableCommand command)
        {
            var response = await MediatR.Send(command);
            return CreateResponse(response);
        }
    }
}
