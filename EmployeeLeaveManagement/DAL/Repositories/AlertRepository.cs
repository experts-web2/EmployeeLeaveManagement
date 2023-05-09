using DAL.Configrations;
using DAL.Interface;
using DomainEntity.Models;
using ELM.Helper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace DAL.Repositories
{
    public class AlertRepository : GenericRepository<Alert>, IAlertRepository
    {
        private readonly AppDbContext _dbContext;
        public AlertRepository(AppDbContext dbContext):base (dbContext)
        {
            _dbContext = dbContext;

        }
        //public void GetAllAbsentEmployee()
        //{
        //    RecurringJob.AddOrUpdate("AlertRecurringJob", () => AddAbsentEmployeeAlert(), "0 0 * * MON-FRI");

        //}
        //public List<Alert> GetAlerts()
        //{
        //    return _dbContext.Alerts.Where(X => X.AlertDate.Date == DateTime.Now.Date).ToList();
        //}

        //public PagedList<Alert> GetAllAlert(Pager pager, Expression<Func<Alert, bool>> predicate = null)
        //{
        //    if (predicate == null)
        //        predicate = PredicateBuilder.True<Alert>();
        //    else
        //        predicate = predicate.And(predicate);

        //    var Alerts = _dbContext.Alerts.Include(x => x.Employee).AsQueryable();


        //    if (!string.IsNullOrEmpty(pager.Search))
        //    {
        //        predicate = predicate.And(x => x.EmployeeId.ToString().Contains(pager.Search.Trim()) ||
        //                   x.Employee.FirstName.Contains(pager.Search.Trim()));
        //    }
        //    if (pager.StartDate != (DateTime.Now.Date) && pager.EndDate != DateTime.MinValue)
        //    {
        //        predicate = predicate.And(x => x.AlertDate.Date >= pager.StartDate.Value.Date && x.AlertDate.Date >= pager.EndDate);
        //    }
        //    //if (pager.EndDate != DateTime.MinValue)
        //    //{
        //    //    predicate = predicate.And(x => x.AlertDate.Date <= pager.EndDate.Value.Date);
        //    //}
        //    else
        //    {
        //        predicate = predicate.And(x => x.AlertDate <= pager.EndDate);
        //    }
        //    Alerts = Alerts.
        //        Where(predicate);


        //    var paginatedList = PagedList<Alert>.ToPagedList(Alerts, pager.CurrentPage, pager.PageSize);
        //    return new PagedList<Alert>
        //        (paginatedList, paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);

        //}

        //public List<Alert> AddAbsentEmployeeAlert()
        //{
        //    //Querry For getting Employees Whose are Absent
        //    var AbsentEmployees = (from Employees in _dbContext.Employees
        //                           join Attendences in _dbContext.Attendences.Where(x => x.AttendenceDate.Date.Equals(new DateTime(2023, 4, 10)) || x.TimeIn == null || x.Timeout == null) on Employees.Id equals Attendences.EmployeeId
        //                           into employeeAtendence
        //                           from attendence in employeeAtendence.DefaultIfEmpty()
        //                           where attendence == null

        //                           select Employees).Include(x => x.Attendences).ToList();
        //    List<Alert> Alerts = new List<Alert>();

        //    foreach (var Employee in AbsentEmployees)
        //    {
        //        Alert Alert = new Alert()
        //        {
        //            AlertDate = DateTime.Now,
        //            AlertType = SetAlertType(Employee),
        //            EmployeeId = Employee.Id,

        //        };
        //        Alerts.Add(Alert);
        //    }
        //    _dbContext.Alerts.AddRange(Alerts);
        //    _dbContext.SaveChanges();
        //    return Alerts;
        //}
        //public string SetAlertType(Employee employee)
        //{

        //    if (employee.Attendences.Any(x => x.Timeout == null))
        //        return "TimeOut Missing";
        //    if (employee.Attendences.Any(x => x.TimeIn == null))
        //        return "TimeIn Missing";
        //    return "Absent";

        //}

        //public List<Alert> GetAlertsByEmployeeId(int id)
        //{
          
        //    var alerts= _dbContext.Alerts.Where(X => X.EmployeeId == id ).Include(x=>x.Employee).ToList();
        //    return alerts;

        //}
    }
}
