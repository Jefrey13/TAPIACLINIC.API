using Application.Models;
using MediatR;

namespace Application.Queries.Surgeries
{
    /// <summary>
    /// Query to get a Surgery by its ID.
    /// </summary>
    public class GetSurgeryByIdQuery : IRequest<SurgeryDto>
    {
        public int Id { get; set; }

        public GetSurgeryByIdQuery(int id)
        {
            Id = id;
        }
    }
}