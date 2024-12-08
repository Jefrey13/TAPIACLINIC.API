using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    /// <summary>
    /// Validación personalizada para fechas, asegura que la fecha no sea mayor a la actual.
    /// </summary>
    public class CustomDateValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Valida que la fecha no sea mayor a la actual.
        /// </summary>
        /// <param name="value">El valor a validar.</param>
        /// <param name="validationContext">Contexto de validación.</param>
        /// <returns>Resultado de validación.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue > DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage ?? "La fecha no puede ser mayor a la actual.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
