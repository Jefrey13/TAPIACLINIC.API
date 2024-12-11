using Application.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestDtos
{
    /// <summary>
    /// DTO para representar los datos para crear o actualizar un usuario.
    /// </summary>
    public class UserRequestDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El apellido no debe exceder los 100 caracteres.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre de usuario no debe exceder los 100 caracteres.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }

        [MaxLength(255, ErrorMessage = "La dirección no debe exceder los 255 caracteres.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, un número y un carácter especial.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [RegularExpression(@"^\+505(5|7|8)\d{6}$",
            ErrorMessage = "El número de teléfono debe comenzar con +505, seguido de 8 dígitos que inicien con 5, 7 o 8.")]
        public string PhoneAddress { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        [RegularExpression(@"^(Male|Female)$", ErrorMessage = "El género debe ser 'Male' o 'Female'.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [CustomDateValidation(ErrorMessage = "La fecha de nacimiento no puede ser mayor a la fecha actual.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "El número de cédula es obligatorio.")]
        [RegularExpression(@"^\d{3}-\d{6}-\d{4}[A-Z]$",
            ErrorMessage = "El formato de la cédula debe ser 000-000000-0000X.")]
        public string IdCard { get; set; }
        public int? RoleId { get; set; }

        public byte[]? ProfileImage { get; set; }
    }
}