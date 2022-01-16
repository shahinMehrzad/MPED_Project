using Microsoft.EntityFrameworkCore;
using MPED.Application.Interfaces.Repositories;
using MPED.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPED.Infrastructure.Repositories
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly IRepositoryAsync<Rooms> _repository;

        public RoomsRepository(IRepositoryAsync<Rooms> repository)
        {
            _repository = repository;
        }

        public async Task DeleteAsync(Rooms room)
        {
            await _repository.DeleteAsync(room);
        }

        public async Task<Rooms> GetByIdAsync(int roomId)
        {
            return await _repository.GetByIdAsync(roomId);
        }

        public async Task<List<Rooms>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Rooms room)
        {
            await _repository.AddAsync(room);
            return room.Id;
        }

        public async Task UpdateAsync(Rooms room)
        {
            await _repository.UpdateAsync(room);
        }
    }
}
