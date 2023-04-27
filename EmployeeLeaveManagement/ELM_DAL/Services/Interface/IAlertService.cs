using DomainEntity.Models;
using ELM.Helper;

namespace ELM_DAL.Services.Interface
{
    public interface IAlertService
    {
        public Task<Response<Alert>> GetAlerts(Pager Paging);
        public Task  DeleteAlert(int? id);
    }
}
