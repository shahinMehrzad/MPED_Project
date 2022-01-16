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
    }
}
