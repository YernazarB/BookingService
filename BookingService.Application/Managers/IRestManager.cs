using BookingService.Domain.Entities;

namespace BookingService.Application.Managers
{
    public interface IRestManager
    {
        Task OnArrive(Group group);
        Task OnLeave(int groupId);
    }
}
