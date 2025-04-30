using peliculas_api.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace peliculas_api.DTOs
{
    public class GeneroCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")] // Atributto de Limitación de netCore
        [StringLength(50, ErrorMessage = "El campo {0} solo admite {1} caracteres")]
        [PrimeraLetraMayuscula]
        public required string Nombre { get; set; }
    }

}
