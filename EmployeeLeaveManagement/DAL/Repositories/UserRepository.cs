using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using ELM.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public Task<IdentityResult> AddUser(RegisterDto model)
        {
            try
            {
                var newUser = new User { UserName = model.Email, Email = model.Email };

                if (model.EmployeeId is not null)
                    newUser.EmployeeId = model.EmployeeId;
                var result = _userManager.CreateAsync(newUser, model.Password);
                if (!result.IsCompleted)
                    return result;
                _userManager.AddToRoleAsync(newUser, "User");
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<User> GetAllUser()
        {
            return _userManager.Users.ToList();
        }
        public List<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }
         
        public async Task<string> SignIn(LogIn login)
        {
            try
            {
                //string error = "User Email or Password is Invalid";
                Demo.Error = "User Email or Password is Invalid";
                var result =   _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
                if (result is not null)
                {
                    var user = await _signInManager.UserManager.FindByEmailAsync(login.Email);
                    var roles = await _signInManager.UserManager.GetRolesAsync(user);
                    var claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.Name, login.Email));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.EmployeeId.ToString()));

                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        _configuration["JWT:ValidIssuer"],
                        _configuration["JWT:ValidAudience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(15),
                        signingCredentials: creds
                    );
                    var Token = new JwtSecurityTokenHandler().WriteToken(token);
                    Demo.Success = Token;
                    return Demo.Success;
                }
                // Response.Cookies.Append("jwt", Token);
                else
                    return Demo.Error;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<bool> DeleteUser(string id)
        {
            var result = await _userManager.DeleteAsync(new User { Id = id });
            if (result.Succeeded) return true;
            return false;
        }
        public async Task<bool> UpdateUser(User user)
        {
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return true;
            return false;
        }
    }
}
public static class Demo
    {
    public  static string Error { get; set; } = string.Empty;
    public static string Success { get; set; } = string.Empty;
}
