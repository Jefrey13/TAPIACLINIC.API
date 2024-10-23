using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Roles
{
    public class GetRoleIdByNameQuery : IRequest<int?>
    {
        public string RoleName { get; }

        public GetRoleIdByNameQuery(string roleName)
        {
            RoleName = roleName;
        }
    }
}
