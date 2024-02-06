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
        private readonly IDailyTimeSheetRepository _dailyTimeSheetRepository;
        private readonly IDailyTimeSheetService _dailyTimeSheetService;
        public DailyTaskService(IDailyTaskRepository dailyTaskRepository, IDailyTimeSheetRepository dailyTimeSheetRepository, IDailyTimeSheetService dailyTimeSheetService)
        {
            _dailyTaskRepository = dailyTaskRepository;
            _dailyTimeSheetRepository = dailyTimeSheetRepository;
            _dailyTimeSheetService = dailyTimeSheetService;
        }

        public DailyTaskDto AddDailyTask(DailyTaskDto dailyTaskDto)
        {
            var response = setDailyTaskEntity(dailyTaskDto);
            var dailyTask = _dailyTaskRepository.Add(response);
            var dbDailyTimeSheet = _dailyTimeSheetService.GetAllDailyTimeSheetByEmployeeId(dailyTaskDto.EmployeeId).FirstOrDefault();
            if (dbDailyTimeSheet != null)
            {
                 _dailyTimeSheetService.UpdateDailyTimeSheet(new DailyTimeSheetDto() {ID = dbDailyTimeSheet.ID, EmployeeId = dailyTaskDto.EmployeeId, TotalTime = dailyTaskDto.TaskTime });
            }
            else
            {
                 _dailyTimeSheetService.AddDailyTimeSheet(new DailyTimeSheetDto() { EmployeeId = dailyTaskDto.EmployeeId, TotalTime = dailyTaskDto.TaskTime});
            }
           
            
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
            var EmployeeTask = _dailyTaskRepository.Get(x=>x.EmployeeId == employeeId && x.CreatedDate.Value.Date == DateTime.Today.Date).Includes(x=>x.DailyTimeSheet).Include(y=>y.Employee);
            return EmployeeTask.Select(setDailyTaskDto).ToList();
        }

        public DailyTaskDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public DailyTaskDto Update(DailyTaskDto dailyTaskDto)
        {
            if (dailyTaskDto.ID > 0)
            {
                var DbDailyTask = _dailyTaskRepository.GetByID(dailyTaskDto.ID);
                var response = setDailyTaskEntity(dailyTaskDto, DbDailyTask);
                _dailyTaskRepository.update(response);
                return setDailyTaskDto(response);
            }
            return new();
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
            DbDailyTask.DailyTimeSheetId = dailyTaskDto.DailyTimeSheetId;
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
                DailyTimeSheetId = dailyTask.DailyTimeSheetId,
                EmployeeName = dailyTask.Employee != null ? dailyTask.Employee.FirstName : ""
            };

        }
    }
}
