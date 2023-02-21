using DAL.Interface;
using ELM.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        // [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetAllUsers()
        {
            var Users = _userRepository.GetAllUser();
            return Ok(Users);
        }
        [HttpGet("GeTAllRoles")]
        public IActionResult GetAllRoles()
        {
            var roles = _userRepository.GetAllRoles();
            return Ok(roles);
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteUser(string id)
        {
            var DeleteUserResponse = _userRepository.DeleteUser(id);
            return Ok(DeleteUserResponse);
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegistrationModel register)
        {
            try
            {
                await _userRepository.AddUser(register);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> Login(LogIn signIn)
        {
            try
            {
                var tokenRecive = await _userRepository.SignIn(signIn);
                Response.Cookies.Append("jwt", tokenRecive, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(1),
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                });
                return Ok("Successfuly Loggedin");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _userRepository.SignOut();
            Response.Cookies.Delete("jwt");
            return Ok("SignOut Succesfully");
        }
    }
}
