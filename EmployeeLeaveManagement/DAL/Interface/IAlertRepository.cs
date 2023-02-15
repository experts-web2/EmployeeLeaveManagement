using DomainEntity.Models;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IAlertRepository
    {
        public List<Alert> GetAlerts();
        List<Alert> AddAbsentEmployeeAlert();
    }
}
