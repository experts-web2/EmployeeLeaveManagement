using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IAttendenceRepository
    {
        List<AttendenceDto> GetAllAttendences();
        bool  AddAttendence(AttendenceDto attendenceDto);
        bool DeleteAttendence(int id);
        AttendenceDto GetById(int id);
        bool Update(AttendenceDto attendenceDto);
    }
}
