using DomainEntity.Models;
using DTOs;
using ELM.Helper;

namespace ELM_DAL.Services.Interface
{
    public interface IAlertService
    {
        Task<Response<Alert>> GetAlerts(Pager Paging);
        Task<IReadOnlyDictionary<int, string>> GetAlertsHavingEmployeeId();
        Task DeleteAlert(int id, List<DateTime> attendenceDates);
        Task<List<Alert>> GetAllAlertsByEmployeeId(int id);
        Task<AlertDto> GetAlertById(int id);
        Task UpdateAlert(AlertDto alert);
    }
}
