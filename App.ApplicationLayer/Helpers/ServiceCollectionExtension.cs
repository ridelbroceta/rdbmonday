using App.ApplicationLayer.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationLayer.Helpers
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RidelTestContext>(
                opts => opts.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(typeof(RidelTestContext)
                    .Assembly.FullName)
                )
            );

            //services.AddDbContext<MainDbContext>(
            //    opts => opts.UseOracle(connectionString,
            //        b => b.MigrationsAssembly(typeof(MainDbContext)
            //        .Assembly.FullName)
            //        .UseOracleSQLCompatibility("12")

            //    ).ConfigureOracle()
            //    //, ServiceLifetime.Scoped by default is scoped
            //);


            //services.AddMediatR(Assembly.GetExecutingAssembly());

            //services.AddTransient<IRepository, Repository>();
            //services.AddTransient<IRepositoryExt, RepositoryExt>();


            return services;
        }
    }
}
