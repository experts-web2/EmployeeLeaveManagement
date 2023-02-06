﻿using DTOs;
using ELM.Web.Services.Interface;

namespace ELM.Web.Services.ServiceRepo
{
    public class AttendenceService : IAttendenceService

    {
        private HttpClient _httpService;
        private string controllerRoute = "https://localhost:7150/api/AttendenceApi";
        public AttendenceService(HttpClient httpService)
        {
            _httpService = httpService;
            
        }
        public async Task<List<AttendenceDto>> GetAttendences()
        {
         
            List<AttendenceDto> response = new();
            try
            {
                response = await _httpService.GetFromJsonAsync<List<AttendenceDto>>(controllerRoute+ "/GetAllAttendences");
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
            
        }
        public async Task AddAttendence(AttendenceDto attendenceDto)
        {
            var response =await _httpService.PostAsJsonAsync(controllerRoute+ "/AddAttendence", attendenceDto);
        }
        public async Task DeleteAttendence(int id)
        {
            await _httpService.DeleteAsync($"{controllerRoute}/{id}");
        }

        public async Task UpdateAttendence(AttendenceDto attendenceDto)
        {
            await _httpService.PutAsJsonAsync(controllerRoute, attendenceDto);
        }
        public async Task<AttendenceDto> GetByID(int value)
        {
            return await _httpService.GetFromJsonAsync<AttendenceDto>($"{controllerRoute}/GetById/{value}");
        }     
    }
}
