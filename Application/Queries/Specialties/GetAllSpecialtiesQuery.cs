using Application.Models;
using MediatR;

namespace Application.Queries.Specialties
{
    /// <summary>
    /// Query to get all Specialties.
    /// </summary>
    public class GetAllSpecialtiesQuery : IRequest<IEnumerable<SpecialtyDto>>
    {
    }
}