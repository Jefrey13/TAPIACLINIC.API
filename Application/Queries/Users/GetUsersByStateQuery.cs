using Application.Models.ReponseDtos;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Users
{
    public class GetUsersByStateQuery : IRequest<IEnumerable<UserResponseDto>>
    {
        public int StateId { get; set; }

        public GetUsersByStateQuery(int stateId)
        {
            StateId = stateId;
        }
    }
}