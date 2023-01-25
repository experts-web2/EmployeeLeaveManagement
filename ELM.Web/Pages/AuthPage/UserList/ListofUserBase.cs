using DomainEntity.Models;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpLeave.Web.Pages.AuthPage.UserList
{
    public class ListofUserBase : ComponentBase
    {
        [Inject]
        public IRegisterService RegisterService { get; set; }
        public List<User> UserList { get; set; } = new List<User>();
        public User SelectedUser { get; set; } = new User();
        protected override async Task OnInitializedAsync()
        {
            await GetAll();
        }
        public async Task GetAll()
        {
            UserList = await RegisterService.GetAllUserCall();
        }
        public void SetUser(string id)
        {
            SelectedUser = UserList.FirstOrDefault(x=>x.Id==id);
        }
    }
}
