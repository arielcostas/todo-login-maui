using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class TareasController : ControllerBase
{
    private AppDbContext _context;
    public TareasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        var userIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == userIdentifier);
        if (usuario == null)
        {
            return Unauthorized();
        }

        var resp = from t in usuario.Tareas
                   select new { t.TareaId, t.Titulo, t.Completada };

        return Ok(resp);
    }

    [HttpPost]
    public ActionResult Crear([FromBody] CrearTareaInput input)
    {
        var userIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == userIdentifier);
        if (usuario == null)
        {
            return Unauthorized();
        }

        Tarea t = new()
        {
            Completada = false,
            Titulo = input.Titulo,
            Creador = usuario
        };

        usuario.Tareas.Add(t);
        _context.SaveChanges();

        return Ok(new
        {
            id = t.TareaId,
            t.Titulo,
            t.Completada
        });
    }


    [HttpPatch("{id}")]
    public ActionResult Editar(int id, [FromBody] ActualizarTareaInput input)
    {
        var userIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == userIdentifier);
        if (usuario == null)
        {
            return Unauthorized();
        }
        var tarea = usuario.Tareas.FirstOrDefault(t => t.TareaId == id);
        if (tarea == null)
        {
            return NotFound();
        }

        tarea.Titulo = input.Titulo ?? tarea.Titulo;
        tarea.Completada = input.Completada ?? tarea.Completada;

        _context.SaveChanges();
        return Ok(new
        {
            id = tarea.TareaId,
            tarea.Titulo,
            tarea.Completada
        });
    }

    [HttpDelete("{id}")]
    public ActionResult Eliminar(int id)
    {
        var userIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == userIdentifier);
        if (usuario == null)
        {
            return Unauthorized();
        }
        var tarea = usuario.Tareas.FirstOrDefault(t => t.TareaId == id);
        if (tarea == null)
        {
            return NotFound();
        }

        usuario.Tareas.Remove(tarea);
        _context.SaveChanges();

        return NoContent();
    }
}

public class CrearTareaInput
{
    public string Titulo { get; set; }
}

public class ActualizarTareaInput
{
    public string? Titulo { get; set; }
    public bool? Completada { get; set; }
}