using DAL.Interface;
using DomainEntity.Models;
using Hangfire;


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
           return _dbContext.Alerts.Where(X=>X.AlertDate.Date==DateTime.Now.Date).ToList();
           
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
