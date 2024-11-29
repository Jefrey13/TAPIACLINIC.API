using Application.Models.ReponseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Staffs
{
    public class GetByRoleQuery : IRequest<IEnumerable<StaffResponseDto>>
    {
        public string RoleName { get; set; }

        public GetByRoleQuery(string roleName)
        {
            RoleName = roleName;
        }
    }
}
