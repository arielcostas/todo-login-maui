using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Usuario
{
    [Key]
    public string Id { get; set; }
    public string Contraseña { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }

    public List<Tarea> Tareas { get; set; }
}
