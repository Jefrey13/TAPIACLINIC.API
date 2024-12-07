using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestDtos
{
    /// <summary>
    /// Represents the contact details for sending a message.
    /// </summary>
    public class ContactRequestDto
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        public string FullName { get; set; }

        [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Los detalles de la consulta son obligatorios.")]
        [StringLength(500, ErrorMessage = "La consulta no puede exceder los 500 caracteres.")]
        public string Consultation { get; set; }
    }
}