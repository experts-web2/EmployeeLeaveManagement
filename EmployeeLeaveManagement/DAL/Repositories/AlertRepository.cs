using DAL.Interface;
using DomainEntity.Models;
using ELM.Helper;
using Hangfire;
using Microsoft.EntityFrameworkCore;


namespace DAL.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private readonly AppDbContext _dbContext;
        public AlertRepository(AppDbContext dbContext)
        {
            _dbContext=dbContext;
              
        }
        public void GetAllAbsentEmployee()
        {
            RecurringJob.AddOrUpdate("AlertRecurringJob", () => AddAbsentEmployeeAlert(), "0 0 * * MON-FRI");

        }
        public List<Alert> GetAlerts()
        {
            return _dbContext.Alerts.Where(X => X.AlertDate.Date == DateTime.Now.Date).ToList();
        }

        public PagedList<Alert> GetAllAlert(Pager pager)
        {
            var Alerts = _dbContext.Alerts.Include(x=>x.Employee).AsQueryable();
            if(!string.IsNullOrEmpty(pager.search) && pager.StartDate != null && pager.EndDate != null)
            {
                Alerts = Alerts.
                    Where(x => (x.EmployeeId.ToString().Contains(pager.search.Trim()) ||
                           x.Employee.FirstName.Contains(pager.search.Trim())) &&
                            x.AlertDate.Date >= pager.StartDate.Value.Date &&
                             x.AlertDate.Date <= pager.EndDate.Value.Date);

            }
           else if(!string.IsNullOrEmpty(pager.search) && pager.StartDate != null)
            {
                Alerts = Alerts.
                    Where(x => (x.EmployeeId.ToString().Contains(pager.search.Trim()) ||
                           x.Employee.FirstName.Contains(pager.search.Trim())) &&
                            x.AlertDate.Date >= pager.StartDate.Value.Date);
            }
           else if(!string.IsNullOrEmpty(pager.search)  && pager.EndDate != null)
            {
                Alerts = Alerts.
                    Where(x => (x.EmployeeId.ToString().Contains(pager.search.Trim()) ||
                           x.Employee.FirstName.Contains(pager.search.Trim())) &&
                             x.AlertDate.Date <= pager.EndDate.Value.Date);
            }
           else if(pager.StartDate != null && pager.EndDate != null)
            {
                Alerts = Alerts.
                 Where(x => x.AlertDate.Date >= pager.StartDate.Value.Date &&
                          x.AlertDate.Date <= pager.EndDate.Value.Date);
            }

           else if (!string.IsNullOrEmpty(pager.search))
            {
                Alerts = Alerts.
                    Where(x => x.EmployeeId.ToString().Contains(pager.search.Trim()) ||
                           x.Employee.FirstName.Contains(pager.search.Trim()));             
            }
           else if(pager.StartDate != null)
            {
                Alerts = Alerts.Where(x => x.AlertDate.Date >= pager.StartDate.Value.Date);
            }
          else if (pager.EndDate != null)
            {
                Alerts = Alerts.Where(x => x.AlertDate.Date <= pager.EndDate.Value.Date);
            }

            var paginatedList = PagedList<Alert>.ToPagedList(Alerts, pager.CurrentPage, pager.PageSize);
            return new PagedList<Alert>
                (paginatedList, paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);
           
        }

        public List<Alert> AddAbsentEmployeeAlert()
        {
            //Querry For getting Employees Whose are Absent
            var AbsentEmployees = (from Employees in _dbContext.Employees
                                  join Attendences in _dbContext.Attendences.Where(x => x.AttendenceDate.Date.Equals(DateTime.Now.Date)) on Employees.Id equals Attendences.EmployeeId
                                  into employeeAtendence
                                  from attendence in employeeAtendence.DefaultIfEmpty()
                                  where attendence == null

                                  select Employees).ToList();
            List<Alert> Alerts = new List<Alert>();
            foreach (var Employee in AbsentEmployees)
            {
              Alert  Alert = new Alert()
                {
                    AlertDate = DateTime.Now,
                    AlertType = "Absent Alert",
                    EmployeeId = Employee.Id,
  
                };
                Alerts.Add(Alert);
            }
            _dbContext.Alerts.AddRange(Alerts);
            _dbContext.SaveChanges();
            return Alerts;
        }
    }
}
