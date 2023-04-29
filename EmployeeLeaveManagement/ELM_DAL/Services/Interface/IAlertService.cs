using DomainEntity.Models;
using ELM.Helper;

namespace ELM_DAL.Services.Interface
{
    public interface IAlertService
    {
        Task<Response<Alert>> GetAlerts(Pager Paging);
        Task DeleteAlert(int id, List<DateTime> attendenceDates);
        Task<List<Alert>> GetAllAlertsByEmployeeId(int id);
        Task<Alert> GetAlertById(int id);
        Task UpdateAlert(Alert alert);
    }
}
