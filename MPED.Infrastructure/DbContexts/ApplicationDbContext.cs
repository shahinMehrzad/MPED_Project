using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MPED.Infrastructure.DbContexts
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        public IDbConnection Connection => throw new NotImplementedException();

        public bool HasChanges => throw new NotImplementedException();

        public EntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
