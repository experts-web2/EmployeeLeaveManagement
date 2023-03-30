using ELM.Shared;
using ELM_DAL.Services.Interface;
using ELM_DAL.Services.ServiceRepo;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace EmpLeave.Web.Pages.AuthPage.LogCredential
{
    public class SigninBase :ComponentBase
    {
        [Inject]
        private IRegisterService RegisterService { get; set; }
        [Inject]
        private IAuthService authService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public LogIn LogIn { get; set; } = new();
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
    
        public bool isInValid = false ;
        private string Error = "";
        protected override async Task OnInitializedAsync()
        {
            var user = (await authenticationStateTask);
            
        }
        protected async Task OnAfterRenderAsync()
        {
            var result = await authService.Login(LogIn);

            if (result.Successful)
            {
                var returnUrl = new Uri(NavigationManager.Uri).AbsolutePath;
              
                if(returnUrl.Contains("login"))
                     NavigationManager.NavigateTo("/");

                else
                    NavigationManager.NavigateTo(returnUrl);
                isInValid = false;
                StateHasChanged();
            }
            else
            {
                isInValid = true;
            }
        }
        public void Cancel()
        {
            NavigationManager.NavigateTo("/Employeelist");
        }
    }
}
