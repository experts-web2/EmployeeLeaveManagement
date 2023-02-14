using DTOs;
using ELM.Helper;

namespace ELM.Web.Services.Interface
{
    public interface ISalaryHistory
    {
        public  Task AddSalary(SalaryHistoryDto salaryHistoryDto);
        Task<SalaryHistoryDto> GetSalaryById(int id);
        Task<Response<SalaryHistoryDto>> GetSalaries(Pager paginh);
        Task DeleteSalary(int id);
        Task UpdateSalary(SalaryHistoryDto salaryHistoryDto);
    }
}
