using Application.Models;
using Application.Models.ReponseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Menus
{
    public class GetMenusByRoleIdQuery : IRequest<IEnumerable<MenuResponseDto>>
    {
        public int RoleId { get; set; }

        public GetMenusByRoleIdQuery(int roleId)
        {
            RoleId = roleId;
        }
    }
}
