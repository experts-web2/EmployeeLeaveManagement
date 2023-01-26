using DAL.Interface.GenericInterface;
using DAL.Repositories;
using System.Runtime.CompilerServices;

namespace EmpLeave.Api.Extensions.DependencyInjection
{
    public static class RepositoriesConfigure
    {
        public static IServiceCollection AddAndConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
