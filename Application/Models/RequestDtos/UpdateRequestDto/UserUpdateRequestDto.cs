using Application.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestDtos.UpdateRequestDto
{
    /// <summary>
    /// DTO for updating user details with validation.
    /// </summary>
    public class UserUpdateRequestDto
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es requerido.")]
        [MaxLength(100, ErrorMessage = "El apellido no debe exceder los 100 caracteres.")]
        public string LastName { get; set; }

        [MaxLength(255, ErrorMessage = "La dirección no debe exceder los 255 caracteres.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "El género es requerido.")]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "El género debe ser 'Male' o 'Female'.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es requerida.")]
        [CustomDateValidation(ErrorMessage = "La fecha de nacimiento no puede ser mayor a la fecha actual.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "El numero de telefono es obligatorio.")]
        public string PhoneAddress { get; set; }

        [Required(ErrorMessage = "El ID del estado es requerido.")]
        [Range(1, 2, ErrorMessage = "El estado debe ser 1 (Activo) o 2 (Inactivo).")]
        public int StateId { get; set; }

        public byte[]? ProfileImage { get; set; }
    }
}