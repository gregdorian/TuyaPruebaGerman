using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TuyaPruebaGerman.Application.Interfaces;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Infrastructure.Persistence.Contexts;
using TuyaPruebaGerman.Infrastructure.Persistence.Repositories;
using TuyaPruebaGerman.Infrastructure.Persistence.Repository;

namespace TuyaPruebaGerman.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            #region Repositories

            // DInjection

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IOrdenCompraRepositoryAsync, OrdenCompraRepositoryAsync>();
            services.AddTransient<IClienteRepositoryAsync, ClienteRepositoryAsync>();
            services.AddTransient<IProductoRepositoryAsync, ProductoRepositoryAsync>();
            services.AddTransient<ILogisticaRepositoryAsync, LogisticaRepositoryAsync>();

            #endregion Repositories

        }
    }
}