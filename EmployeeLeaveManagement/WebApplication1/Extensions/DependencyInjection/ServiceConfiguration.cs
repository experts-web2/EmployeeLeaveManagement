using BL.Interface;
using BL.Service;

namespace EmpLeave.Api.Extensions.DependencyInjection
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddAndConfigureService(this IServiceCollection services)
        {
            services.AddScoped<ILeaveService, LeaveService>();
            return services;
        }

    }
}
