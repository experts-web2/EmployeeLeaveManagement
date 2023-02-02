﻿using DTOs;

namespace ELM.Web.Services.Interface
{
    public interface IAttendenceService
    {
        Task AddAttendence(AttendenceDto attendenceDto);
        Task<List<AttendenceDto>> GetAttendences();
        Task DeleteAttendence(int id);

    }
}
