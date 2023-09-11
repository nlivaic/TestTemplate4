using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TestTemplate4.Application.Pipelines;

namespace TestTemplate4.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTestTemplate4ApplicationHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
            services.AddPipelines();

            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
