﻿using DAL.Interface.GenericInterface;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ISalaryHistoryRepository:IGenericRepository<SalaryHistory>
    {
    }
}
