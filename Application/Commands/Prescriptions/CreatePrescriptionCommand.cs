using Application.Models.RequestDtos;
using MediatR;

namespace Application.Commands.Prescriptions
{
    /// <summary>
    /// Command to create a new prescription.
    /// </summary>
    public class CreatePrescriptionCommand : IRequest<int>
    {
        public PrescriptionRequestDto PrescriptionDto { get; set; }

        /// <summary>
        /// Initializes a new instance of the CreatePrescriptionCommand class.
        /// </summary>
        /// <param name="prescriptionDto">The DTO containing the details of the prescription to create.</param>
        public CreatePrescriptionCommand(PrescriptionRequestDto prescriptionDto)
        {
            PrescriptionDto = prescriptionDto;
        }
    }
}