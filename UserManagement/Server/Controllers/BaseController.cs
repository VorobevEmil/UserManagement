using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UserManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        internal Guid UserId => !User.Identity!.IsAuthenticated ? Guid.Empty : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    }
}
