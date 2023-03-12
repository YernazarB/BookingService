using BookingService.Domain.Enums;

namespace BookingService.Application.Responses
{
    public record BaseResponse<T> where T : class
    {
        public BaseResponse(T data = null, ResponseCode code = ResponseCode.Ok, string errorMessage = null)
        {
            Data = data;
            ResponseCode = code;
            ErrorMessage = errorMessage;
        }

        public ResponseCode ResponseCode { get; init; }
        public string ErrorMessage { get; init; }
        public bool Success => ResponseCode == ResponseCode.Ok || ResponseCode == ResponseCode.Created;
        public T Data { get; init; }
    }
}
