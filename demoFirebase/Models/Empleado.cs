using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace demoFirebase.Models
{
    public class Empleado
    {
        public int? IdEmpleado { get; set; } = 0;
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
        public string? Nombre { get; set; } = "";
        [Required(ErrorMessage = "El numero es requerido")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "El numero de telefono tiene que terner 10 caracteres")]
        public string? Telefono { get; set; } = "";
        public string? URLimagen { get; set; } = "";
        public string? NombreImagen { get; set; } = "";

    }
}
