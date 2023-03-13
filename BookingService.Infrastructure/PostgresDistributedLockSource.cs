using BookingService.Application.Interfaces;
using Medallion.Threading;
using Medallion.Threading.Postgres;

namespace BookingService.Infrastructure
{
    public class PostgresDistributedLockSource : IDistributedLockSource
    {
        private readonly string _connectionString;
        public PostgresDistributedLockSource(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDistributedLock GetDistributedLock(string lockName)
        {
            return new PostgresDistributedLock(new PostgresAdvisoryLockKey(lockName, allowHashing: true), _connectionString);
        }
    }
}
