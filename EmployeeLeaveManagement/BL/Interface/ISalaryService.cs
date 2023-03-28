using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface ISalaryService
    {
        public void AddSalary(SalaryHistoryDto salaryDto);
        public PagedList<SalaryHistoryDto> GetSalaries(Pager pager, Expression<Func<SalaryHistory, bool>> predicate = null);
        public List<SalaryHistoryDto> GetSalary(int id);
        public SalaryHistoryDto GetSalaryById(int id);
        public void EditSalary(SalaryHistoryDto salarDto);
        public void DeleteSalary(int id);
    }
}
