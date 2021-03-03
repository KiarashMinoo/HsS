using HsS.Ifs.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HsS.Data
{
    public static class StartupConfig
    {
        public static void DatasConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).
                AddEntityFrameworkStores<ApplicationDbContext>();

            UnitOfWork.FillDefinedRepositories();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IDapperContainer, DapperContainer>();
        }
    }
}
