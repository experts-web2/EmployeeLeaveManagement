using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELM_DAL.Services.Interface
{
    public interface IAlertService
    {
        public Task<Response<Alert>> GetAlerts(Pager Paging);
    }
}
