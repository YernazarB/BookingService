using BookingService.Application.Responses;
using BookingService.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ISender MediatR;
        public BaseController(ISender mediatR)
        {
            MediatR = mediatR;
        }

        protected IActionResult CreateResponse<T>(BaseResponse<T> response) where T : class
        {
            switch (response.ResponseCode)
            {
                case ResponseCode.Ok:
                    return Ok(response);
                case ResponseCode.Created:
                    return new ObjectResult(response) { StatusCode = (int)ResponseCode.Created };
                case ResponseCode.BadRequest:
                    return BadRequest(response);
                case ResponseCode.Unauthorized:
                    return Unauthorized(response);
                case ResponseCode.Forbidden:
                    return Forbid();
                case ResponseCode.NotFound:
                    return NotFound();
                default:
                    return BadRequest($"Unhandled status code: {response.ResponseCode}");
            }
        }
    }
}
