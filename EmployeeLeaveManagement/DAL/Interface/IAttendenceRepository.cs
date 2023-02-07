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
        void DeleteAttendence(int id);
        AttendenceDto GetById(int id);
        void Update(AttendenceDto attendenceDto);
    }
}
