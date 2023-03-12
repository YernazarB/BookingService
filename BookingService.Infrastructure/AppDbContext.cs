using BookingService.Application.Interfaces;
using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
