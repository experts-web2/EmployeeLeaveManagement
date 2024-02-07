using BL.Interface;
using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class DailyTimeSheetService : IDailyTimeSheetService
    {
        private readonly IDailyTimeSheetRepository _dailyTimeSheetRepository;
        public DailyTimeSheetService(IDailyTimeSheetRepository dailyTimeSheetRepository)
        {
            _dailyTimeSheetRepository = dailyTimeSheetRepository;
        }
        public DailyTimeSheetDto AddDailyTimeSheet(DailyTimeSheetDto dailyTaskDto)
        {
            var existedDailySheet = GetAllDailyTimeSheetByEmployeeId(dailyTaskDto.EmployeeId).FirstOrDefault();
            if (existedDailySheet != null)
            {
                return null;
            }
            var DailyTimeSheetEntity = setEntity(dailyTaskDto);
            _dailyTimeSheetRepository.Add(DailyTimeSheetEntity);
            return setDailyTimeSheetDto(DailyTimeSheetEntity);
        }

        public DailyTimeSheetDto UpdateDailyTimeSheet(DailyTimeSheetDto dailyTaskDto)
        {
            if (dailyTaskDto.ID > 0)
            {
                var dbDailyTimeSheet = _dailyTimeSheetRepository.GetByID(dailyTaskDto.ID);
                if (dbDailyTimeSheet != null) 
                {
                    var response = setEntity(dailyTaskDto, dbDailyTimeSheet);
                    _dailyTimeSheetRepository.update(response);
                    return dailyTaskDto;
                }
            }
            return new();
        }

        public void DeleteDailyTimesheet(int id)
        {
            throw new NotImplementedException();
        }

        public List<DailyTimeSheetDto> GetAllDailyTimeSheet()
        {
           var allDailySheet = _dailyTimeSheetRepository.Get(x => x.CreatedDate.Value.Date == DateTime.Today.Date).Include(x=>x.DailyTasks).Include(x=>x.Employee);
            return allDailySheet.Select(setDailyTimeSheetDto).ToList();
        }

        public List<DailyTimeSheetDto> GetAllDailyTimeSheetByEmployeeId(int employeeId)
        {
            var dbDailyTimeSheet = _dailyTimeSheetRepository.Get(x => x.EmployeeId == employeeId && x.CreatedDate.Value.Date == DateTime.Today.Date).Include(x=>x.DailyTasks).Include(x => x.Employee);
            return dbDailyTimeSheet.Select(setDailyTimeSheetDto).ToList();
        }

        public DailyTimeSheetDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        private DailyTimeSheet setEntity(DailyTimeSheetDto dailyTimeSheetDto, DailyTimeSheet dbDailyTimeSheet = null)
        {
            if (dbDailyTimeSheet == null)
            {
                dbDailyTimeSheet = new DailyTimeSheet()
                {
                    CreatedDate = DateTime.Now,
                    TotalTime = dailyTimeSheetDto.TotalTime
                };
            }
            else
            {
                dbDailyTimeSheet.ModifiedDate = DateTime.Now;
                dbDailyTimeSheet.TotalTime += dailyTimeSheetDto.TotalTime;
            }

            dbDailyTimeSheet.EmployeeId = dailyTimeSheetDto.EmployeeId;
            return dbDailyTimeSheet;
        }

        public DailyTimeSheetDto setDailyTimeSheetDto(DailyTimeSheet dailyTimeSheet)
        {
            return new()
            {
                EmployeeId = dailyTimeSheet.EmployeeId,
                TotalTime = dailyTimeSheet.TotalTime,
                ID = dailyTimeSheet.Id,
                CreatedDate = dailyTimeSheet.CreatedDate,
                EmployeeName = dailyTimeSheet.Employee != null ? dailyTimeSheet.Employee.FirstName : string.Empty,
                dailyTaskDtos = dailyTimeSheet.DailyTasks != null ? dailyTimeSheet.DailyTasks!.Select(setDailyTaskDto).ToList() : null,
            };

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
