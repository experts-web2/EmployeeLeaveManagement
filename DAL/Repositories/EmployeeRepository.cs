﻿using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext Db;

        public EmployeeRepository(AppDbContext db)
        {
            Db = db;
        }

        public void AddEmployee(EmployeeDto employee)
        {
            Employee employeeEntity = ToEntity(employee);
            Db.Employees.Add(employeeEntity);
            Db.SaveChanges();

        }
        private Employee ToEntity(EmployeeDto employeeDto)
        {
            Employee employee = new Employee()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Address = employeeDto.Address,
                DateOfBrith = employeeDto.DateOfBrith,
                Gender = employeeDto.Gender,
                Email = employeeDto.Email

            };
            return employee;
        }
        public List<EmployeeDto> GetAllEmployee()
        {
            var employees = Db.Employees.Include(x => x.Leaves).ToList();
            List<EmployeeDto> employeeDto=ToDtos(employees);
            return employeeDto;
        }
        private List<EmployeeDto> ToDtos(List<Employee> employees)
        {
            try
            {
                List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
                foreach (var employee in employees)
                {
                    EmployeeDto employeeDto = new EmployeeDto();
                    employeeDto.ID = employee.Id;
                    employeeDto.FirstName= employee.FirstName;
                    employeeDto.LastName = employee.LastName;
                    employeeDto.Address = employee.Address;
                    employeeDto.DateOfBrith = employee.DateOfBrith;
                    employeeDto.Email = employee.Email;
                    employeeDto.Gender = employee.Gender;
                    employeeDto.leaves = employee.Leaves.ToList();
                   
                    employeeDtos.Add(employeeDto);
                }
                return employeeDtos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteEmployee(int id)
        {
            try
            {
                var Deleted = Db.Employees.FirstOrDefault(x => x.Id == id);
                if (Deleted != null)
                {
                    Db.Remove(Deleted);
                    Db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public void Update(EmployeeDto employee)
        {
            if(employee != null)
            try
            {
                 var Updated = ToEntity(employee);
                Db.Update(Updated);
                Db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
