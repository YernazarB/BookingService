using BookingService.Application.Interfaces;
using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Application.Managers
{
    public class RestManager : IRestManager
    {
        private readonly IAppDbContext _db;
        private readonly Medallion.Threading.IDistributedLock _lock;
        private const string REST_MANAGER_LOCK_NAME = "RestManagerLock";
        public RestManager(IAppDbContext db, IDistributedLockSource _lockProvider)
        {
            _db = db;
            _lock = _lockProvider.GetDistributedLock(REST_MANAGER_LOCK_NAME);
        }

        public async Task OnArrive(Group newGroup)
        {
            await using (await _lock.AcquireAsync())
            {
                //Seek an empty table
                var table = await _db.Tables.Where(x => x.Remainder == x.Size && x.Size >= newGroup.Size).OrderBy(x => x.Size).FirstOrDefaultAsync();

                //If there is no empty table, seek another available table
                if (table == null)
                {
                    table = await _db.Tables.FirstOrDefaultAsync(x => x.Remainder >= newGroup.Size);
                }

                //Book the table if it is not null
                if (table != null)
                {
                    newGroup.TableId = table.Id;
                    table.Occupied += newGroup.Size;
                    table.Remainder -= newGroup.Size;
                }

                await _db.Groups.AddAsync(newGroup);
                await _db.SaveChangesAsync();
            }
        }

        public async Task OnLeave(int groupId)
        {
            await using (await _lock.AcquireAsync())
            {
                var group = await _db.Groups.Where(x => x.Id == groupId).Include(x => x.Table).FirstOrDefaultAsync();
                if (group == null)
                {
                    return;
                }

                //Release the table
                var table = group.Table;
                if (table != null)
                {
                    table.Occupied -= group.Size;
                    table.Remainder += group.Size;
                }

                //Remove the group
                _db.Groups.Remove(group);
                await _db.SaveChangesAsync();

                //Add pending groups to the table
                if (table != null)
                {
                    var pendingGroup = await GetPendingGroup(table);
                    while (pendingGroup != null)
                    {
                        table.Occupied += pendingGroup.Size;
                        table.Remainder -= pendingGroup.Size;
                        pendingGroup.TableId = table.Id;

                        await _db.SaveChangesAsync();
                        pendingGroup = await GetPendingGroup(table);
                    }
                }
            }
        }

        private async Task<Group> GetPendingGroup(Table table)
        {
            var group = await _db.Groups
                .Where(x => !x.TableId.HasValue && x.Size <= table.Remainder)
                .OrderBy(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            return group;
        }
    }
}
