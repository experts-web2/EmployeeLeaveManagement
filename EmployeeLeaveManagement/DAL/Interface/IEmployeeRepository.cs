﻿using DomainEntity.Models;
using DomainEntity.Pagination;
using DTOs;
using System;
using System.Linq.Expressions;

namespace DAL.Interface
{
    public interface IEmployeeRepository
    {
        PagedList<EmployeeDto> GetAllEmployee(Pager pager);
        void AddEmployee(EmployeeDto employee);
        void DeleteEmployee(int id);

        void Update(EmployeeDto employee);
        EmployeeDto GetById(int id);

    }
}