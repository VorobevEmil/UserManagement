using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserManagement.Server.Models.DbModels;
using UserManagement.Shared.Models.Account;

namespace UserManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Пользователь вышел из системы");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Данные не валидны");
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok("Пользователь успешно авторизирован");
                }
            }
            return Conflict("Неправильный логин или пароль, пожалуйста проверьте правильность набора данных");
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Данные не валидны");
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null || await _userManager.FindByNameAsync(model.Username) != null)
            {
                return Conflict("Пользователь уже существует в системе");
            }

            User user = new User { Email = model.Email, UserName = model.Username };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok("Пользователь успешно зарегистрирован");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return StatusCode(500, "Не удалось зарегистрировать пользователя, пожалуйста повторите попытку позже");
        }

        [HttpGet("GetCurrentUserData")]
        public ActionResult<UserInfo> GetCurrentUserData()
        {
            UserInfo userInfo = new UserInfo();
            if (User.Identity!.IsAuthenticated)
            {
                userInfo.AuthenticationType = User.Identity!.AuthenticationType!;
                userInfo.Claims = User.Claims.Select(t => new ApiClaim(t.Type, t.Value)).ToList();
            }
            return Ok(userInfo);
        }
    }
}
