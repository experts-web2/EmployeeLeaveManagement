using DomainEntity.Models;
using ELM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface IAlertService
    {
        public PagedList<Alert> GetAllAlert(Pager pager, Expression<Func<Alert, bool>> predicate = null);
        List<Alert> AddAbsentEmployeeAlert();
        public List<Alert> GetAlertsByEmployeeId(int id);
        public void DeleteAlertByEmployeeId(int employeeId);
    }
}
