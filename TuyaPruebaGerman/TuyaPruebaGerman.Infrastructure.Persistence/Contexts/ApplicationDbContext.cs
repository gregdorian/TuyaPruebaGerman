using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Interfaces;
using TuyaPruebaGerman.Domain.Common;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly ILoggerFactory _loggerFactory;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTime,
            ILoggerFactory loggerFactory
            ) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _loggerFactory = loggerFactory;
        }

        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<OrdenCompra> OrdenesCompras { get; set; }
        public virtual DbSet<Logistica> Logisticas { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var _mockData = this.Database.GetService<IMockService>();
            var seedProducto = _mockData.SeedProducto(1000);
            var seedCliente = _mockData.SeedCliente(1000);
            builder.Entity<Producto>().HasData(seedProducto);
            builder.Entity<Cliente>().HasData(seedCliente);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}