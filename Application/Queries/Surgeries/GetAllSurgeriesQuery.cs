using Application.Models;
using MediatR;

namespace Application.Queries.Surgeries
{
    /// <summary>
    /// Query to get all Surgeries.
    /// </summary>
    public class GetAllSurgeriesQuery : IRequest<IEnumerable<SurgeryDto>>
    {
    }
}