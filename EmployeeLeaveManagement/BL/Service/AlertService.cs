using BL.Interface;
using DAL;
using DAL.Configrations;
using DAL.Interface;
using DAL.Repositories;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        private readonly IEmployeeService _employeeService;

        public AlertService(IAlertRepository alertRepository, IEmployeeService employeeService)
        {
            _alertRepository = alertRepository;
            _employeeService = employeeService;
        }
        public PagedList<Alert> GetAllAlert(Pager pager, Expression<Func<Alert, bool>> predicate = null)
        {
            try
            {
                if (predicate == null)
                    predicate = PredicateBuilder.True<Alert>();
                else
                    predicate = predicate.And(predicate);

                var Alerts = _alertRepository.Get(x =>x.isDeleted == false).Include(x => x.Employee).AsQueryable();


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


                var paginatedList = PagedList<Alert>.ToPagedList(Alerts.OrderByDescending(x => x.AlertDate), pager.CurrentPage, pager.PageSize);
                return new PagedList<Alert>
                    (paginatedList, paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Alert> AddAbsentEmployeeAlert()
        {
            try
            {
                List<Employee> AbsentEmployees = _employeeService.GetAbsentEmployees();

                List<Alert> Alerts = new List<Alert>();

                foreach (var Employee in AbsentEmployees)
                {
                    Alert Alert = new Alert()
                    {
                        AlertDate = DateTime.Now,
                        AlertType = SetAlertType(Employee),
                        EmployeeId = Employee.Id,

                    };
                    var alreadyInserted = _alertRepository.Get(x => x.AlertDate.Date == Alert.AlertDate.Date && x.EmployeeId == Alert.EmployeeId).FirstOrDefault();
                    if (alreadyInserted == null)
                       if(!Alert.AlertType.Contains("Attendence Marked"))
                           Alerts.Add(Alert);
                }
                if (Alerts is null || !Alerts.Any())
                    return new List<Alert>();
                _alertRepository.AddRange(Alerts);
                return Alerts;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public string SetAlertType(Employee employee)
        {
            try
            {
                var toadayAttendence = employee.Attendences.FirstOrDefault(x => x.AttendenceDate.Date == DateTime.Now.Date);
                if (toadayAttendence == null)
                    return "Absent";
                if (toadayAttendence.TimeIn == null)
                    return "TimeIn Missing";
                if (toadayAttendence.Timeout == null)
                    return "TimeOut Missing";
                 return "Attendence Marked";

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Alert> GetAlertsByEmployeeId(int id)
        {
            try
            {
                var alerts = _alertRepository.Get(x => x.EmployeeId == id && x.isDeleted ==false, x => x.Employee).ToList();
                return alerts;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void DeleteAlertByEmployeeId(int employeeId,DateTime attendenceDate)
        {
            try
            {
                _alertRepository.DeleteAlertByEmployeeId(x => x.EmployeeId == employeeId && x.AlertDate.Date == attendenceDate);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public AlertDto GetAlertById(int id)
        {
            try
            {
                var alert = _alertRepository.GetByID(id);
                if(alert == null)
                    return new AlertDto();
                AlertDto alertDto = ToDto(alert);
                return alertDto;
            }
            catch (Exception)
            {

                throw;
            }
          
        }
        public void UpdateAlert(AlertDto alertDto)
        {
            try
            {
                Alert alert = ToEntity(alertDto);
                _alertRepository.update(alert);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public IReadOnlyDictionary<int, string> GetAlertsHavingEmployeeId()
        {
            try
            {
                Dictionary<int, string> employeesToReturn = new();
                IQueryable<Employee> employees = _alertRepository.Get(x => x.AlertDate <= DateTime.Now, y => y.Employee).Select(z => z.Employee);
                foreach (Employee employee in employees)
                {
                    if (!employeesToReturn.ContainsKey(employee.Id))
                    {
                        employeesToReturn.Add(employee.Id, employee.FirstName);
                    }
                }

                return employeesToReturn;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AlertDto> GetAlertByAttendenceDateAndEmployeeId(DateTime attendenceDate, int employeeId)
        {
            try
            {
                var alert = await _alertRepository.Get(x => x.AlertDate.Date == attendenceDate && x.EmployeeId == employeeId).FirstOrDefaultAsync();
                if(alert == null)
                    return new AlertDto();
                AlertDto alertDto = ToDto(alert);
                return alertDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private AlertDto ToDto(Alert alert)
         {
            try
            {
                if (alert == null)
                    return new AlertDto();
                AlertDto alertDto = new()
                {
                    ID = alert.Id,
                    AlertDate = alert.AlertDate,
                    AlertType = alert.AlertType,
                    isDeleted = alert.isDeleted,
                    EmployeeId = alert.EmployeeId,
                    CreatedBy = alert.CreatedBy,
                    CreatedDate = alert.CreatedDate,
                    ModifiedBy = alert.ModifiedBy,
                    ModifiedDate = alert.ModifiedDate
                };
                return alertDto;
            }
            catch (Exception)
            {

                throw;
            }
         }
        private Alert ToEntity(AlertDto alertDto)
        {
            try
            {
                if (alertDto == null)
                    return new Alert();
                Alert alert = new()
                {
                    Id = alertDto.ID,
                    AlertDate = alertDto.AlertDate,
                    AlertType = alertDto.AlertType,
                    isDeleted = alertDto.isDeleted,
                    EmployeeId = alertDto.EmployeeId,
                    CreatedBy = alertDto.CreatedBy,
                    CreatedDate = alertDto.CreatedDate,
                    ModifiedBy = alertDto.ModifiedBy,
                    ModifiedDate = alertDto.ModifiedDate
                };
                return alert;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
