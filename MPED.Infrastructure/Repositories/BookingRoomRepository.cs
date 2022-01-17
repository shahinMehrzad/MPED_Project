using MPED.Application.Interfaces.Repositories;
using MPED.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPED.Infrastructure.Repositories
{
    internal class BookingRoomRepository : IBookingRoomRepository
    {
        private readonly IRepositoryAsync<BookingRoom> _repository;
        public BookingRoomRepository(IRepositoryAsync<BookingRoom> repository)
        {
            _repository = repository;
        }

        public async Task<BookingRoom> GetByIdAsync(int bookingId)
        {
            return await _repository.GetByIdAsync(bookingId);
        }

        public async Task<List<int>> GetReservedRoomsAsync(DateTime from, DateTime to)
        {
            var lst = await _repository.GetAllAsync(x => x.EndDate.Date >= from.Date);
            return lst.Where(x => (from.Date > x.StartDate.Date && from.Date < x.EndDate.Date) || (to.Date > x.StartDate.Date && to.Date <= x.EndDate.Date)).Select(x=>x.RoomId).Distinct().ToList();
        }

        public Task<List<BookingRoom>> GetListByRoomIdAsync(int roomId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> InsertAsync(BookingRoom bookingRoom)
        {
            await _repository.AddAsync(bookingRoom);
            return bookingRoom.Id;
        }

        public async Task UpdateAsync(BookingRoom bookingRoom)
        {
            await _repository.UpdateAsync(bookingRoom);
        }
    }
}
