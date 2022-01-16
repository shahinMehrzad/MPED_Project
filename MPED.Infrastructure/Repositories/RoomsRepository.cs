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

        public async Task<List<Rooms>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }
    }
}
