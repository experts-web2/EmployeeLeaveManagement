using BL.Interface;
using DAL.Interface;
using DAL.Repositories;
using DomainEntity.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class DailyTaskService : IDailyTaskService
    {
        private readonly IDailyTaskRepository _dailyTaskRepository;
        public DailyTaskService(IDailyTaskRepository dailyTaskRepository)
        {
            _dailyTaskRepository = dailyTaskRepository;
        }

        public DailyTaskDto AddDailyTask(DailyTaskDto dailyTaskDto)
        {
            var response = setDailyTaskEntity(dailyTaskDto);
            var dailyTask =  _dailyTaskRepository.Add(response);
            return setDailyTaskDto(dailyTask);
        }

        public void DeleteDailyTask(int id)
        {
            throw new NotImplementedException();
        }

        public List<DailyTaskDto> GetAllDailyTask()
        {
           var DailyTasks = _dailyTaskRepository.Get(x=>x.CreatedDate.Value.Date == DateTime.Today.Date).Include(x=>x.Employee);
            return DailyTasks.Select(setDailyTaskDto).ToList();
        }

        public List<DailyTaskDto> GetAllDailyTaskByEmployeeId(int employeeId)
        {
            var EmployeeTask = _dailyTaskRepository.Get(x=>x.EmployeeId == employeeId && x.CreatedDate.Value.Date == DateTime.Today.Date).Includes(x=>x.Employee);
            return EmployeeTask.Select(setDailyTaskDto).ToList();
        }

        public DailyTaskDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public DailyTaskDto Update(DailyTaskDto dailyTaskDto)
        {
            throw new NotImplementedException();
        }

        private DailyTask setDailyTaskEntity(DailyTaskDto dailyTaskDto, DailyTask DbDailyTask = null)
        {
            if (DbDailyTask == null)
            {
                DbDailyTask = new DailyTask() 
                { 
                    CreatedDate = DateTime.Now
                };
            }
            else
            {
                DbDailyTask.ModifiedDate = DateTime.Now;
            }

            DbDailyTask.EmployeeId = dailyTaskDto.EmployeeId;
            DbDailyTask.TaskName = dailyTaskDto.TaskName;
            DbDailyTask.TaskTime = dailyTaskDto.TaskTime;
            DbDailyTask.Description = dailyTaskDto.Description;
            return DbDailyTask;
        }

        private DailyTaskDto setDailyTaskDto(DailyTask dailyTask)
        {
            return new()
            {
                TaskName = dailyTask.TaskName,
                TaskTime = dailyTask.TaskTime,
                Description = dailyTask.Description,
                CreatedDate = dailyTask.CreatedDate,
                EmployeeName = dailyTask.Employee != null ? dailyTask.Employee.FirstName : ""
            };

        }
    }
}
