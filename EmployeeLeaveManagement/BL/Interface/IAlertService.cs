using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using System.Linq.Expressions;

namespace BL.Interface;

public interface IAlertService
{
    PagedList<Alert> GetAllAlert(Pager pager, Expression<Func<Alert, bool>> predicate = null);
    List<Alert> AddAbsentEmployeeAlert();
    List<Alert> GetAlertsByEmployeeId(int id);
    AlertDto GetAlertById(int id);
    void UpdateAlert(AlertDto alert);
    void DeleteAlertByEmployeeId(int employeeId ,DateTime attendenceDate);
    IReadOnlyDictionary<int, string> GetAlertsHavingEmployeeId();
    Task<AlertDto> GetAlertByAttendenceDateAndEmployeeId(DateTime attendenceDate,int employeeId); 
}