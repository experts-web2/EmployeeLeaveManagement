using DTOs;
using ELM_DAL.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace ELM.Web.Pages.DailTimeSheet
{
    public class DailyTimeSheetBase :  ComponentBase
    {
        [Inject]
        public IDailyTimeSheetService dailyTimeSheetService { get; set; }
        public DailyTimeSheetDto DailyTimeSheetDto { get; set; } = new DailyTimeSheetDto();

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();   
        }

        public async Task AddDailyTimeSheet()
        {
          await  dailyTimeSheetService.AddDailyTimeSheet(DailyTimeSheetDto);
        }
    }
}
