using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Server.Common.Exceptions;
using UserManagement.Server.Interfaces;
using UserManagement.Server.Models.DbModels;

namespace UserManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ManagementUserController : BaseController
    {
        private readonly IManagementUserService _service;

        public ManagementUserController(IManagementUserService service)
        {
            _service = service;
        }

        [HttpGet("GetById/{userId}")]
        public async Task<ActionResult<User>> GetById(string userId, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(userId, cancellationToken));
            }
            catch (NotFoundException)
            {
                return NotFound("Пользователь не найден");
            }
            catch
            {
                return BadRequest("Не удалось получить пользователя");
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<User>>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetAllAsync(cancellationToken));
            }
            catch (Exception)
            {
                return BadRequest("Не удалось получить пользователей");
            }
        }

        [HttpDelete("Delete/userId")]
        public async Task<IActionResult> Delete(string userId, CancellationToken cancellationToken)
        {
            try
            {
                await _service.DeleteAsync(userId, cancellationToken);
                return Ok("Пользователь удален");
            }
            catch (NotFoundException)
            {
                return NotFound("Пользователь не найден");
            }
            catch
            {
                return BadRequest("Не удалось удалить пользователя");
            }
        }

        [HttpPost("RefreshStatusBlock")]
        public async Task<IActionResult> RefreshStatusBlock(bool refresh, string userId, CancellationToken cancellationToken)
        {
            try
            {
                await _service.RefreshStatusBlockAsync(refresh, userId, cancellationToken);
                return Ok("Статус блокировки пользователя обновлен");
            }
            catch (NotFoundException)
            {
                return NotFound("Пользователь не найден");
            }
            catch
            {
                return BadRequest("Не удалось обновить статус блокировки пользователя");
            }
        }
    }
}
