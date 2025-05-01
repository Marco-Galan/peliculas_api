using peliculas_api.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace peliculas_api.DTOs
{
    public class GeneroDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

    }
}
