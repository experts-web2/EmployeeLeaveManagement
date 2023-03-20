using DAL;
using DomainEntity.Models;
using DTOs;
using ELM.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32.SafeHandles;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmpLeave.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager,
                              SignInManager<User> signInManager, IConfiguration configuration,AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Post([FromBody] RegisterDto model)
        {
            try
            {
                var newUser = new User { UserName = model.Email, Email = model.Email};

                if (model.EmployeeId is not null)
                    newUser.EmployeeId = model.EmployeeId;

                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(x => x.Description);

                    return BadRequest(new RegisterResult { Successful = false, Errors = errors });
                }

                // Add all new users to the User

                await _userManager.AddToRoleAsync(newUser, "User");

                return Ok(new RegisterResult { Successful = true });
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllUsers()
        {
            var users=_userManager.Users.ToList();
            return Ok(users);

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
       
        public async Task<IActionResult> Login([FromBody] LogIn login)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

                if (!result.Succeeded) return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });

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
                AttendenceApiController.AccessToken = Token;
                Response.Cookies.Append("jwt", Token);
                return Ok(Token);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Content("You Are Successfully Logout");
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<ActionResult>DeleteUser(string id)
        {
            var deleteUser=_context.Users.FirstOrDefault(x => x.Id == id);
            if(deleteUser != null)
            {
                _context.Remove(deleteUser);
                _context.SaveChanges();
            }
            return Ok();
        }
    }
}
