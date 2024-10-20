using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.States
{
    /// <summary>
    /// Query to retrieve all states.
    /// </summary>
    public class GetAllStatesQuery : IRequest<IEnumerable<StateDto>>
    {
    }
}
