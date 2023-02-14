using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ISalaryHistoryRepository
    {
        public void AddSalary(SalaryHistoryDto salaryDto);
        public List<SalaryHistoryDto> GetSalaries();
        public SalaryHistoryDto GetSalary(int id);
        public void EditSalary(SalaryHistoryDto salarDto);
        public void DeleteSalary(int id);
    }
}
