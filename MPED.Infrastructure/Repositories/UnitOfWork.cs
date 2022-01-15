using MPED.Application.Interfaces.Repositories;
using MPED.Application.Interfaces.Shared;
using MPED.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MPED.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly ApplicationDbContext _dbContext;
        private bool disposed;

        public UnitOfWork(ApplicationDbContext dbContext, IAuthenticatedUserService authenticatedUserService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<int> Commit(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task Rollback()
        {
            //todo
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }
    }
}
