using Application.Models;
using MediatR;

namespace Application.Queries.Specialties
{
    /// <summary>
    /// Query to get a Specialty by its ID.
    /// </summary>
    public class GetSpecialtyByIdQuery : IRequest<SpecialtyDto>
    {
        public int Id { get; set; }

        public GetSpecialtyByIdQuery(int id)
        {
            Id = id;
        }
    }
}