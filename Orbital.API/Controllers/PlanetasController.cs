using Microsoft.AspNetCore.Mvc;

namespace Orbital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanetasController : ControllerBase
{
    [HttpGet]
    public IActionResult GetPlanetas()
    {
        var planetas = new[]
        {
            new { id = 1, nombre = "Namek", nivelAmenaza = 8 },
            new { id = 2, nombre = "Vegeta", nivelAmenaza = 6 }
        };

        return Ok(planetas);
    }
}