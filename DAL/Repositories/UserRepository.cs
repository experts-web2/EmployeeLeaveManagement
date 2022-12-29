﻿using DAL.Interface;
using DomainEntity.Models;
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
        public List<User> GetAllUser()
        {
          return  _userManager.Users.ToList();
        }
        public async Task AddUser(UserRegistrationModel registerDto)
        {
            try
            {
                User user = new()
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName

                };
                IdentityResult identityResult = await _userManager.CreateAsync(user, registerDto.Password);
                if (!identityResult.Succeeded) throw new InvalidOperationException($"Error: {string.Join("\n", identityResult.Errors.Select(x => x.Description))}");

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> SignIn(SignIn signIn)
        {

            var result = await _signInManager.PasswordSignInAsync(signIn.Email, signIn.Password, false, false);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException();

            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,signIn.Email),

                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };
            var authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var Token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken
            (

                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(20),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
       public async Task SignOut()
        {

            await _signInManager.SignOutAsync();
        } 
        public async Task<bool> DeleteUser(string id)
        {
          var result= await _userManager.DeleteAsync(new User { Id=id});
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
