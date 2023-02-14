using DTOs;

namespace ELM.Web.Services.Interface
{
    public interface ISalaryHistory
    {
        public  Task AddSalary(SalaryHistoryDto salaryHistoryDto);
        Task<SalaryHistoryDto> GetSalaryById(int id);
        Task<List<SalaryHistoryDto>> GetSalaries();
        Task DeleteSalary(int id);
        Task UpdateSalary(SalaryHistoryDto salaryHistoryDto);
    }
}
