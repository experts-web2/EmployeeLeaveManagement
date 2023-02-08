using DTOs;

namespace ELM.Web.Services.Interface
{
    public interface ISalaryHistory
    {
        public  Task AddSalary(SalaryHistoryDto salaryHistoryDto);
        Task<SalaryHistoryDto> GetSalaryById(int id);
    }
}
