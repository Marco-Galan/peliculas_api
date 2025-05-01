using peliculas_api.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace peliculas_api.entidades
{
    public class Genero
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} solo admite {1} caracteres")]
        [PrimeraLetraMayuscula]
        public required string Nombre { get; set; }

    }
}
