using System.ComponentModel.DataAnnotations;

namespace peliculas_api.Validaciones
{
    public class PrimeraLetraMayusculaAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primeraLetra = value.ToString()![0].ToString();

            if(primeraLetra != primeraLetra.ToUpper())
            {
                return new ValidationResult("La primera letra debe ser ayuscula");
            }
            return ValidationResult.Success;
        }
    }
}
