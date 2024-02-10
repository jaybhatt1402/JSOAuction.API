using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Data.Repositories;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Services.Interfaces;
using JSOAuction.Utility;
using Microsoft.EntityFrameworkCore;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace JSOAuction.API.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            var assembliesToScan = new[]{
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(IBaseService))
            };

            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
                 .Where(c => c.Name.EndsWith("Service"))
                 .AsPublicImplementedInterfaces();
        }
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IAccountsRepository<>), typeof(AccountsRepository<>));
            services.AddTransient(typeof(IPlayerRegisterRepository<>), typeof(PlayerRegisterRepository<>));
            services.AddTransient(typeof(IRefreshTokenRepository<>), typeof(RefreshTokenRepository<>));
            services.AddTransient(typeof(ITeamRegisterRepository<>), typeof(TeamRegisterRepository<>));
            services.AddTransient(typeof(IAuctionRegisterRepository<>), typeof(AuctionRegisterRepository<>));

        }

        public static void ConfigureDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MasterDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(Constants.MasterDBConnectionName));
            });

            //DependencyResolver.Current.GetService<IAuthProviderService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            services.AddDbContext<AuditLogDbContext>(options => { });

            services.AddDbContext<ReadWriteApplicationDbContext>(options => { });
            services.AddDbContext<ReadOnlyApplicationDbContext>(options => { });

            services.AddScoped<Func<ReadOnlyApplicationDbContext>>((provider) => () => provider.GetService<ReadOnlyApplicationDbContext>());
            services.AddScoped<Func<ReadWriteApplicationDbContext>>((provider) => () => provider.GetService<ReadWriteApplicationDbContext>());
            services.AddScoped<Func<AuditLogDbContext>>((provider) => () => provider.GetService<AuditLogDbContext>());
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            //var urls = settings.ClientAppUrl.Split(",", StringSplitOptions.RemoveEmptyEntries);

            services.AddCors(options =>
            {
                options.AddPolicy("JSOAuctionCors", b =>
                {
                    b.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:3000")
                    .SetIsOriginAllowed((host) => true)
                    .WithExposedHeaders("Content-Disposition");

				});
            });
        }
    }
}
