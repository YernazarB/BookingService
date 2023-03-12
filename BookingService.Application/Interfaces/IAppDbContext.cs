using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Table> Tables { get; }
        DbSet<Group> Groups { get; }
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
