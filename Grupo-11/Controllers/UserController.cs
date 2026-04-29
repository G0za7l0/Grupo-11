using Microsoft.AspNetCore.Mvc;
using Grupo11.Security;
using Grupo11.Security.Model;
using Grupo11.Security.Entity;

namespace Grupo11.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [AuthController(Permissions.CanViewUsers)]
        public IActionResult GetUsers()
        {
            return Ok(new { message = "Tienes permiso para ver usuarios", data = "Lista de usuarios (mock)" });
        }

        [HttpPost]
        [AuthController(Permissions.CanEditUsers)]
        public IActionResult CreateUser([FromBody] Security_Users user)
        {
            return Ok(new { message = "Usuario creado (mock)", user });
        }
    }
}
