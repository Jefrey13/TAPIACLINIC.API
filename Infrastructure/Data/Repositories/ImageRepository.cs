using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ImageRepository : BaseRepository<Image>, IImageRepository
{
    public ImageRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Image>> GetImagesByReferenceIdAsync(int referenceId)
    {
        return await _context.Images
            .Where(i => i.ReferenceId == referenceId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Image>> GetImagesByTypeAsync(string type)
    {
        return await _context.Images
            .Where(i => i.Type == type)
            .ToListAsync();
    }
}