using Microsoft.EntityFrameworkCore;
using MPED.Infrastructure.DbContexts;
using MPED_Project.Models;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace MPED_Project.Services.RefreshTokenRepositories
{
    public class DatabaseRefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IdentityContext _context;
        public DatabaseRefreshTokenRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task Create(RefreshToken refreshToken)
        {
            var model = new MPED.Application.DTOs.Identity.RefreshToken()
            {
                Id = refreshToken.Id,
                Token = refreshToken.Token,
                UserId = Guid.Parse(refreshToken.UserId)
            };
            var result = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Id == model.Id);
            if (result != null)
            {
                await Delete(model.Id);
            }
            _context.RefreshTokens.Add(model);
            await _context.SaveChangesAsync();
        }

        public Task Create(MPED.Application.DTOs.Identity.RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid id)
        {
            var refreshToken = await _context.RefreshTokens.FindAsync(id);
            if (refreshToken != null)
            {
                _context.RefreshTokens.Remove(refreshToken);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAll(Guid userId)
        {
            var refreshTokens = await _context.RefreshTokens
                .Where(t => t.UserId == userId)
                .ToListAsync();

            _context.RefreshTokens.RemoveRange(refreshTokens);
            await _context.SaveChangesAsync();
        }


        public async Task<Models.RefreshToken> GetByToken(string token)
        {
            var result = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
            if (result == null)
            {
                return null;
            }
            return new Models.RefreshToken()
            {
                Id = result.Id,
                Token = result.Token,
                UserId = result.UserId.ToString()
            };
        }     
    }
}
