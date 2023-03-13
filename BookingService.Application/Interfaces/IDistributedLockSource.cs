using Medallion.Threading;

namespace BookingService.Application.Interfaces
{
    public interface IDistributedLockSource
    {
        IDistributedLock GetDistributedLock(string lockName);
    }
}
