using Application.Commands.MedicalRecords;
using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IMedicalRecordAppService
    {
        Task<int> CreateMedicalRecordAsync(CreateMedicalRecordCommand command);
        Task UpdateMedicalRecordAsync(UpdateMedicalRecordCommand command);
        Task DeleteMedicalRecordAsync(int id);
        Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecordsAsync();
        Task<MedicalRecordDto> GetMedicalRecordByIdAsync(int id);
    }
}