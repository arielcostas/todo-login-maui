using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Tarea
{
    [Key]
    public int TareaId { get; set; }

    public string Titulo { get; set; }
    
    public bool Completada { get; set; }

    public Usuario Creador { get; set; }

    public Tarea()
    {
    }
}
