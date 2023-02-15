using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IAlertRepository
    {
        public PagedList<Alert> GetAllAlert(Pager pager);
        List<Alert> AddAbsentEmployeeAlert();
    }
}
