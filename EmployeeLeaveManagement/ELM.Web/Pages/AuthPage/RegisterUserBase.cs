using DomainEntity.Models;
using DTOs;
using ELM.Shared;
using EmpLeave.Web.Services.Interface;
using EmpLeave.Web.Services.ServiceRepo;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EmpLeave.Web.Pages.AuthPage
{
    public class RegisterUserBase :ComponentBase
    {
        [Inject]
        private IRegisterService RegisterService { get; set; }
        public UserRegistrationModel RegisterModel { get; set; } = new();
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<IdentityRole> Roles { get; set; } = new ();
        protected override async Task OnInitializedAsync()
        {
           Roles=  await RegisterService.GetAllRoles();
        }
        protected async Task SetRolesOnSelect(ChangeEventArgs e)
        {
            RegisterModel.Roles = ((string[])e.Value).ToList();
        }
        protected async Task SaveEmployee()
        {
           
            await RegisterService.AddUserCall(RegisterModel);
           
            Cancel();
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("/addemployee");
        }
    }
}
