using Domain.Entities;

namespace Domain.Repositories; 
public interface IImageRepository : IRepository<Image>
{
    Task<IEnumerable<Image>> GetImagesByReferenceIdAsync(int referenceId);
    Task<IEnumerable<Image>> GetImagesByTypeAsync(string type);
}