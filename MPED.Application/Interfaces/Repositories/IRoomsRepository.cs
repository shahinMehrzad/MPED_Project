using MPED.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPED.Application.Interfaces.Repositories
{
    public interface IRoomsRepository
    {
        Task<List<Rooms>> GetListAsync();
        Task<Rooms> GetByIdAsync(int roomId);
        Task<int> InsertAsync(Rooms room);
        Task UpdateAsync(Rooms room);
        Task DeleteAsync(Rooms room);
    }
}
