using DomainEntity.Models;
using DTOs;
using ELM.Helper;

namespace ELM_DAL.Services.Interface
{
    public interface IAlertService
    {
        Task<Response<Alert>> GetAlerts(Pager Paging);
        Task<List<Alert>> GetAllAlertsByEmployeeId(int id);
        Task<AlertDto> GetAlertById(int id);
        Task UpdateAlert(AlertDto alert);
        Task<AlertDto> GetAlertByAttendenceDateAndEmployeeId(DateTime attendenceDate,int employeeId);
    }
}
