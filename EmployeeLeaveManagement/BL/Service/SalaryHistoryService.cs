using BL.Interface;
using DAL.Configrations;
using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class SalaryHistoryService:ISalaryHistoryService
    {
        private readonly ISalaryHistoryRepository _salaryHistoryRepository;
        private readonly ISalaryRepository _salaryRepository;
        public SalaryHistoryService(ISalaryHistoryRepository salaryHistoryRepository, ISalaryRepository salaryRepository)
        {
            _salaryHistoryRepository = salaryHistoryRepository;
            _salaryRepository = salaryRepository;
        }

        public void AddSalary(SalaryHistoryDto salaryDto)
        {
            try
            {
                Salary salaryResponse = new();
                SalaryHistory salary = ToEntity(salaryDto);
                _salaryHistoryRepository.Add(salary);
                var employeeSalaryExisted = _salaryRepository.GetByID(salaryDto.EmployeeId!.Value);
                if (employeeSalaryExisted != null)
                {
                    salaryResponse = AddOrUpdateSalary(salaryDto, employeeSalaryExisted);
                    _salaryRepository.update(salaryResponse);
                }
                else
                {
                    salaryResponse = AddOrUpdateSalary(salaryDto);
                    _salaryRepository.Add(salaryResponse);
                }
                //_salaryRepository.Add(new Salary() { CurrentSalary = (decimal)salary.NewSalary, TotalSalary = (decimal)salary.NewSalary, EmployeeId = salary.EmployeeId!.Value});
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Salary AddOrUpdateSalary(SalaryHistoryDto salaryDto, Salary? DbSalary = null)
        {
            if (DbSalary is null)
            {
                DbSalary = new Salary() 
                { 
                    CreatedDate = DateTime.Now
                };
            }
            else
            {
                DbSalary.ModifiedDate = DateTime.Now;
            }
            DbSalary.EmployeeId = salaryDto.EmployeeId!.Value;
            DbSalary.CurrentSalary = (decimal)salaryDto.NewSalary;
            DbSalary.TotalSalary = (decimal)salaryDto.NewSalary;
            return DbSalary;
        }

        public void DeleteSalary(int id)
        {
            try
            {
                 _salaryHistoryRepository.deletebyid(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EditSalary(SalaryHistoryDto salaryDto)
        {
            try
            {
                var salary = _salaryHistoryRepository.GetByID(salaryDto.ID);
                 ToEntity(salary,salaryDto);
                _salaryHistoryRepository.update(salary);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ToEntity(SalaryHistory salary, SalaryHistoryDto salaryDto)
        {
            try
            {
                salary.NewSalary = salaryDto.NewSalary;
                salary.IncrementDate = salaryDto.IncrementDate;
                salary.EmployeeId = salaryDto.EmployeeId;
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public PagedList<SalaryHistoryDto> GetSalaries(Pager pager, Expression<Func<SalaryHistory, bool>> predicate = null)
        {
            try
            {
                if (predicate == null)
                    predicate = PredicateBuilder.True<SalaryHistory>();
                else
                    predicate = predicate.And(predicate);
                IQueryable<SalaryHistory> salaries = _salaryHistoryRepository.GetAll().Include(x => x.Employee);
                if (!string.IsNullOrEmpty(pager.Search))
                {
                    predicate = predicate.And(x => x.EmployeeId.ToString() == pager.Search);
                }
                if (pager.StartDate?.Date != DateTime.Now.Date && pager.EndDate?.Date != DateTime.MinValue)
                {
                    predicate = predicate.And(x => x.IncrementDate.Date >= pager.StartDate && x.IncrementDate.Date <= pager.EndDate);
                }
                else
                    predicate = predicate.And(x => x.IncrementDate.Date <= pager.EndDate);
                salaries = salaries.Where(predicate);
                var paginatedList = PagedList<SalaryHistory>.ToPagedList(salaries, pager.CurrentPage, pager.PageSize);
                var SalariesDto = ToDto(paginatedList);
                return new PagedList<SalaryHistoryDto>
                    (SalariesDto, paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public SalaryHistoryDto GetSalaryById(int id)
        {
            try
            {
                var salary = _salaryHistoryRepository.Get(x => x.Id == id, x => x.Employee).FirstOrDefault();
                if (salary == null) return new SalaryHistoryDto();
                SalaryHistoryDto salaryDto = SetToDto(salary);
                return salaryDto;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<SalaryHistoryDto> GetSalary(int id)
        {
            try
            {
                List<SalaryHistory> salary = _salaryHistoryRepository.GetAll().Include(s => s.Employee).ThenInclude(x=>x.Leaves).Where(x => x.EmployeeId == id).ToList();
                if (salary == null)
                    return new List<SalaryHistoryDto>();
                List<SalaryHistoryDto> salaryDto = SetSalaryToDto(salary);
                return salaryDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SalaryHistory> GetSalaryHistoryByEmployeeId(int id)
        {
            try
            {
                List<SalaryHistory> salary = _salaryHistoryRepository.GetAll().Include(s => s.Employee).ThenInclude(x => x.Leaves).Where(x => x.EmployeeId == id).ToList();
                if (salary == null)
                    return new List<SalaryHistory>();
                return salary;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private SalaryHistory ToEntity(SalaryHistoryDto salaryDto)
        {
            SalaryHistory salary = new()
            {
                Id = salaryDto.ID,
                NewSalary = salaryDto.NewSalary,
                IncrementDate = salaryDto.IncrementDate,
                EmployeeId = salaryDto.EmployeeId
            };
            return salary;
        }
        private List<SalaryHistoryDto> ToDto(List<SalaryHistory> salaries)
        {
            List<SalaryHistoryDto> salariesDto = new();
            foreach (var salary in salaries)
            {
                SalaryHistoryDto salaryDto = new()
                {
                    ID = salary.Id,
                    NewSalary = salary.NewSalary,
                    IncrementDate = salary.IncrementDate,
                    FirstName = salary.Employee.FirstName,
                    LastName = salary.Employee.LastName
                };
                salariesDto.Add(salaryDto);
            }
            return salariesDto;
        }
        private List<SalaryHistoryDto> SetSalaryToDto(List<SalaryHistory> salaries)
        {
            List<SalaryHistoryDto> salaires = new List<SalaryHistoryDto>();
            foreach (var salary in salaries)
            {
                SalaryHistoryDto salaryDto = new()
                {
                    ID = salary.Id,
                    NewSalary = salary.NewSalary,
                    IncrementDate = salary.IncrementDate,
                    FirstName = salary.Employee.FirstName,
                    LastName = salary.Employee.LastName,
                    EmployeeId = salary.EmployeeId
                };
                salaires.Add(salaryDto);
            }
            return salaires;
        }
        private SalaryHistoryDto SetToDto(SalaryHistory salary)
        {
            SalaryHistoryDto salaryDto = new()
            {
                ID = salary.Id,
                NewSalary = salary.NewSalary,
                IncrementDate = salary.IncrementDate,
                FirstName = salary.Employee.FirstName,
                LastName = salary.Employee.LastName,
                EmployeeId = salary.EmployeeId
            };
            return salaryDto;
        }
    }
}
