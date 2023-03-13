using BookingService.Domain.Entities;

namespace BookingService.Application.Managers
{
    public interface IRestManager
    {
        /// <summary>
        /// Creates a new group and makes a reservation for this new group.
        /// </summary>
        /// <param name="newGroup"></param>
        /// <returns></returns>
        Task OnArrive(Group newGroup);
        /// <summary>
        /// Removes the group and releases a table occupied by this group. Returns released table's id.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<int?> OnLeave(int groupId);
        /// <summary>
        /// Adds corresponding groups from queue to the table
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        Task ReserveTable(int tableId);
    }
}
