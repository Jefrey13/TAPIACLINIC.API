using Application.Models.ReponseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Staffs
{
    public class GetByStateQuery: IRequest<IEnumerable<StaffResponseDto>>
    {
        public int StateId { get; set; }

        public GetByStateQuery(int stateId) 
        {
            StateId = stateId;
        }
    }
}
