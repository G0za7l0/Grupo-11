using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Grupo11.Security;
using Grupo11.Security.Model;

namespace Grupo11.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecurityController : ControllerBase
    {
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest Inst)
        {
            var sessionKey = HttpContext.Session.GetString("sessionKey");
            var result = AuthNetCore.Login(Inst, sessionKey);
            
            if (result != null)
            {
                var dict = result as dynamic;
                string newSession = dict.SessionKey;
                HttpContext.Session.SetString("sessionKey", newSession);
                return Ok(result);
            }
            return Unauthorized("Credenciales inválidas");
        }

        [HttpPost("LogOut")]
        public IActionResult LogOut()
        {
            var sessionKey = HttpContext.Session.GetString("sessionKey");
            AuthNetCore.ClearSeason(sessionKey);
            HttpContext.Session.Remove("sessionKey");
            return Ok(new { message = "Sesión cerrada correctamente" });
        }
    }
}
