using MPED_Project.Models;
using System;
using System.Threading.Tasks;

namespace MPED_Project.Services.RefreshTokenRepositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByToken(string token);

        Task Create(RefreshToken refreshToken);

        Task Delete(Guid id);

        Task DeleteAll(Guid userId);
    }
}
