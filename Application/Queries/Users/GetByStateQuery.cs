using Application.Models.ReponseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users
{
    /// <summary>
    /// Query to retrieve a user by their state ID.
    /// </summary>
    public class GetByStateQuery: IRequest<UserResponseDto>
    {
        public int StateId { get; set; }

        public GetByStateQuery(int stateId)
        {
            StateId = stateId;
        }
    }
}
