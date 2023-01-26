﻿using DTOs;
using ELM.Web.Helper;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpLeave.Web.Pages.EmployeePage
{
    [Authorize]
    public class EmployeeListBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public List<EmployeeDto> EmployeeDtoList { get; set; } = new ();
        public EmployeeDto SelectedEmployee { get; set; } = new();
        public void SetEmployeeID(int id)
        {
            SelectedEmployee = EmployeeDtoList.FirstOrDefault(x => x.ID == id);
        }

        protected override async Task OnInitializedAsync()
        {
          await  GetAll();
        }
        public async Task GetAll()
        {
            Parameter parameter = new();
            var EmployeeDto =await EmployeeService.GetAllEmployee(parameter);
            EmployeeDtoList = EmployeeDto.DataList;
        }
        public void DeleteConfirm(int Id)
        {
            SelectedEmployee = EmployeeDtoList.FirstOrDefault(x => x.ID == Id);
        }

        public async Task DeleteEmployee(int id)
        {
            await EmployeeService.DeleteCall(id);
           await GetAll();
            
        }
    }
}
