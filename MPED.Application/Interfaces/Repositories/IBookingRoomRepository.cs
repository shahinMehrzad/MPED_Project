using MPED.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPED.Application.Interfaces.Repositories
{
    public interface IBookingRoomRepository
    {
        Task<List<BookingRoom>> GetListByRoomIdAsync(int roomId);
        Task<List<int>> GetReservedRoomsAsync(DateTime from, DateTime to);
        Task<BookingRoom> GetByIdAsync(int bookingId);
        Task<int> InsertAsync(BookingRoom bookingRoom);
        Task UpdateAsync(BookingRoom bookingRoom);
    }
}
