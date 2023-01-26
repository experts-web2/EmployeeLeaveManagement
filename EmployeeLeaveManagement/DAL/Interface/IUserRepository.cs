﻿using DomainEntity.Models;
using ELM.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IUserRepository
    { 
        Task AddUser(UserRegistrationModel userRegistrationModel);
        Task<bool> DeleteUser(string id);
        List<IdentityRole> GetAllRoles();
        List<User> GetAllUser();
        Task<string> SignIn(LogIn signIn);
        Task SignOut();
        Task<bool> UpdateUser(User user);
    }
}
