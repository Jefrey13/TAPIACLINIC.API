using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Roles
{
    public class GetRoleIdByNameQueryHandler : IRequestHandler<GetRoleIdByNameQuery, int?>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleIdByNameQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<int?> Handle(GetRoleIdByNameQuery request, CancellationToken cancellationToken)
        {
            // Obtenemos el RoleId por su nombre desde el repositorio
            return await _roleRepository.GetRoleIdByNameAsync(request.RoleName);
        }
    }
}
