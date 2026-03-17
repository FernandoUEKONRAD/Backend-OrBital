using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest(new { message = "Datos incompletos" });
        }

        try
        {
            var user = _context.Usuarios
                .FirstOrDefault(u => u.Username == request.Username 
                                  && u.Password == request.Password); // Nota: Usa contraseñas hash en producción.

            if (user == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            return Ok(new 
            { 
                message = "Login exitoso", 
                userId = user.Id,
                username = user.Username
            });
        }
        catch (Exception ex)
        {
            // Loguea el error (lo guarda en logs)
            return StatusCode(500, new { message = "Ocurrió un error en el servidor" });
        }
    }
}