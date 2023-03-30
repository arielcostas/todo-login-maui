using Api;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApliMovil.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private AppDbContext _context;
    private IPasswordHasher<Usuario> _hasher;

    public AuthController(AppDbContext context)
    {
        _context = context;
        _hasher = new PasswordHasher<Usuario>();
    }

    [HttpPost]
    public ActionResult Registro([FromBody] DatosRegistro datos)
    {
        Usuario u = new Usuario
        {
            Id = datos.Id,
            Nombre = datos.Nombre,
            Apellidos = datos.Apellidos,
            Contraseña = string.Empty
        };

        u.Contraseña = _hasher.HashPassword(u, datos.NuevaContraseña);

        _context.Usuarios.Add(u);
        _context.SaveChanges();

        return Ok(new
        {
            u.Id,
            u.Nombre,
            u.Apellidos
        });
    }

    [HttpPost]
    public ActionResult Login([FromBody] DatosLogin datos)
    {
        var u = _context.Usuarios.FirstOrDefault(u => u.Id == datos.Id);
        if (u == null) return Unauthorized();

        var passwordValidation = _hasher.VerifyHashedPassword(u, u.Contraseña, datos.Contraseña);
        if (passwordValidation == PasswordVerificationResult.Failed)
        {
            return Unauthorized();
        }

        var handler = new JwtSecurityTokenHandler();
        Claim[] claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, u.Id),
            new("nombre", u.Nombre),
            new("apellidos", u.Apellidos)
        };

        var identity = new ClaimsIdentity(claims);

        var descriptor = new SecurityTokenDescriptor()
        {
            Subject = identity,
            SigningCredentials = new(new SymmetricSecurityKey(
                "Z6YC3uQCXQ3hsbY8cWRR"u8.ToArray()
                ), SecurityAlgorithms.HmacSha384),
        };

        var t = handler.CreateToken(descriptor);
        return Ok(new {
            token = handler.WriteToken(t)
        });
    }

    [HttpGet]
    [Authorize]
    public ActionResult Me()
    {
        var claims = User.Claims.ToDictionary(c => c.Type, c => c.Value);

        return Ok(new
        {
            id = claims[ClaimTypes.NameIdentifier],
            nombre = claims["nombre"],
            apellidos = claims["apellidos"],
        });
    }
}

public class DatosRegistro
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Apellidos { get; set; }
    [Required]
    public string NuevaContraseña { get; set; }
}

public class DatosLogin
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string Contraseña { get; set; }
}