using Microsoft.EntityFrameworkCore;
using MPED.Application.Interfaces.Contexts;
using MPED.Application.Interfaces.Shared;
using MPED.Domain;
using MPED.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MPED.Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly DateTime _dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _authenticatedUser = authenticatedUser;
            _dateTime = DateTime.UtcNow;
        }

        #region DbSet
        public DbSet<Audit> AuditLogs { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<BookingRoom>  BookingRooms { get; set; }
        #endregion

        public IDbConnection Connection => Database.GetDbConnection();

        public bool HasChanges = false;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Audit>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DateTime = _dateTime;
                        entry.Entity.UserId = _authenticatedUser.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.DateTime = _dateTime;
                        entry.Entity.UserId = _authenticatedUser.UserId;
                        break;
                }
            }
            //foreach (var entry in ChangeTracker.Entries<BaseEntity>().ToList())
            //{
            //    var entityType = entry.Entity.GetType().Name;
            //    switch (entry.State)
            //    {
            //        case EntityState.Added:
            //            entry.Entity.CreatedOn = _dateTime;
            //            entry.Entity.CreatedBy = _authenticatedUser.UserId;
            //            try
            //            {
            //                await AuditLogs.AddAsync(new Audit
            //                {
            //                    Type = "Added",
            //                    DateTime = _dateTime,
            //                    UserId = _authenticatedUser.UserId,
            //                    TableName = entityType,
            //                    //Values = JsonConvert.SerializeObject(entry.CurrentValues.Properties.ToList()),
            //                    PrimaryKey = JsonConvert.SerializeObject(entry.Entity.Id)
            //                });
            //            }
            //            catch { }

            //            break;
            //        case EntityState.Modified:
            //            entry.Entity.LastModifiedOn = _dateTime;
            //            entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
            //            try
            //            {
            //                await AuditLogs.AddAsync(new Audit
            //                {
            //                    Type = "Modified",
            //                    DateTime = _dateTime,
            //                    UserId = _authenticatedUser.UserId,
            //                    TableName = entityType,
            //                    //Values = JsonConvert.SerializeObject(entry.CurrentValues),
            //                    PrimaryKey = JsonConvert.SerializeObject(entry.Entity.Id)
            //                });
            //            }
            //            catch { }
            //            break;
            //    }
            //}
            return await base.SaveChangesAsync(cancellationToken);
        }
        
    }
}
