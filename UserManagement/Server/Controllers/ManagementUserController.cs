using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Server.Common.Exceptions;
using UserManagement.Server.Interfaces;
using UserManagement.Server.Models.DbModels;
using UserManagement.Shared.Contracts.ManagementUser.Requests;
using UserManagement.Shared.Contracts.ManagementUser.Responses;

namespace UserManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ManagementUserController : BaseController
    {
        private readonly IManagementUserService _service;
        private readonly IMapper _mapper;

        public ManagementUserController(IManagementUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("GetById/{userId}")]
        public async Task<ActionResult<UserResponse>> GetById(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _service.GetByIdAsync(userId, cancellationToken);
                return Ok(_mapper.Map<User, UserResponse>(user));
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
                var users = await _service.GetAllAsync(cancellationToken);
                return Ok(_mapper.Map<List<User>, List<UserResponse>>(users));
            }
            catch (Exception)
            {
                return BadRequest("Не удалось получить пользователей");
            }
        }

        [HttpDelete("Delete/{userId}")]
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
        public async Task<IActionResult> RefreshStatusBlock(UserBlockRequest userBlockRequest, CancellationToken cancellationToken)
        {
            try
            {
                await _service.RefreshStatusBlockAsync(userBlockRequest, cancellationToken);
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
