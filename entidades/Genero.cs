using peliculas_api.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace peliculas_api.entidades
{
    public class Genero
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")] // Atributto de Limitación de netCore
        [StringLength(10, ErrorMessage = "El campo {0} debe tener {1} caracteres o más")]
        [PrimeraLetraMayuscula]
        public required string Nombre { get; set; }
        //[Range(18,120)]

        //public int Edad {  get; set; }
        //[CreditCard] //Validacion definida por framework
        //public string? TarjetaCredito { set; get; }

        //[Url]
        //public string? Url {  get; set; }



    }
}
