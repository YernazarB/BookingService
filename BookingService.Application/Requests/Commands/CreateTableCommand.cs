using BookingService.Application.Responses;
using System.ComponentModel.DataAnnotations;

namespace BookingService.Application.Requests.Commands
{
    public class CreateTableCommand : BaseRequest<TableViewModel>
    {
        [Range(2, 6)]
        public int Size { get; set; }
    }
}
