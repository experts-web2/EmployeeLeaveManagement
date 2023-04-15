using BL.Interface;
using DAL;
using DAL.Configrations;
using DAL.Interface;
using DomainEntity.Models;
using ELM.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;
        private readonly AppDbContext _dbContext;
        public AlertService(IAlertRepository alertRepository, AppDbContext dbContext)
        {
            _alertRepository = alertRepository;
            _dbContext = dbContext;
        }
        public PagedList<Alert> GetAllAlert(Pager pager, Expression<Func<Alert, bool>> predicate = null)
        {
            if (predicate == null)
                predicate = PredicateBuilder.True<Alert>();
            else
                predicate = predicate.And(predicate);

            var Alerts = _dbContext.Alerts.Include(x => x.Employee).AsQueryable();


            if (!string.IsNullOrEmpty(pager.Search))
            {
                predicate = predicate.And(x => x.EmployeeId.ToString().Contains(pager.Search.Trim()) ||
                           x.Employee.FirstName.Contains(pager.Search.Trim()));
            }
            if (pager.StartDate != (DateTime.Now.Date) && pager.EndDate != DateTime.MinValue)
            {
                predicate = predicate.And(x => x.AlertDate.Date >= pager.StartDate.Value.Date && x.AlertDate.Date >= pager.EndDate);
            }
            //if (pager.EndDate != DateTime.MinValue)
            //{
            //    predicate = predicate.And(x => x.AlertDate.Date <= pager.EndDate.Value.Date);
            //}
            else
            {
                predicate = predicate.And(x => x.AlertDate.Date <= pager.EndDate);
            }
            Alerts = Alerts.
                Where(predicate);


            var paginatedList = PagedList<Alert>.ToPagedList(Alerts, pager.CurrentPage, pager.PageSize);
            return new PagedList<Alert>
                (paginatedList, paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);

        }
        public List<Alert> AddAbsentEmployeeAlert()
        {
            //Querry For getting Employees Whose are Absent
            var AbsentEmployees = (from Employees in _dbContext.Employees
                                   join Attendences in _dbContext.Attendences
                                                                 .Where(x => x.AttendenceDate.Date.Equals(DateTime.Now)
                                                                 )
                                                    on Employees.Id equals Attendences.EmployeeId
                                   into employeeAtendence
                                   from attendence in employeeAtendence.DefaultIfEmpty()
                                   where attendence == null || attendence.Timeout == null
                                   select Employees).Include(x => x.Attendences).ToList();
            List<Alert> Alerts = new List<Alert>();

            foreach (var Employee in AbsentEmployees)
            {
                Alert Alert = new Alert()
                {
                    AlertDate = DateTime.Now,
                    AlertType = SetAlertType(Employee),
                    EmployeeId = Employee.Id,

                };
                var alreadyInserted = _dbContext.Alerts.FirstOrDefault(x => x.AlertDate.Date == Alert.AlertDate.Date&&x.EmployeeId==Alert.EmployeeId);
                if (alreadyInserted == null)
                    Alerts.Add(Alert);
            }
            _dbContext.Alerts.AddRange(Alerts);
            _dbContext.SaveChanges();
            return Alerts;
        }
        public string SetAlertType(Employee employee)
        {

            if (employee.Attendences.Any(x => x.Timeout == null))
                return "TimeOut Missing";
            if (employee.Attendences.Any(x => x.TimeIn == null))
                return "TimeIn Missing";
            return "Absent";

        }
        public List<Alert> GetAlertsByEmployeeId(int id)
        {

            var alerts = _dbContext.Alerts.Where(X => X.EmployeeId == id).Include(x => x.Employee).ToList();
            return alerts;

        }
    }
}
