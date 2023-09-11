using Microsoft.Extensions.DependencyInjection;
using TestTemplate4.Common.Interfaces;
using TestTemplate4.Core.Interfaces;
using TestTemplate4.Data.Repositories;

namespace TestTemplate4.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSpecificRepositories(this IServiceCollection services) =>
            services.AddScoped<IFooRepository, FooRepository>();

        public static void AddGenericRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
