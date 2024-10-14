//public class ManageRolesAndPermissionsService
//{
//    private readonly IRoleRepository _roleRepository;
//    private readonly IPermissionRepository _permissionRepository;

//    public ManageRolesAndPermissionsService(IRoleRepository roleRepository, IPermissionRepository permissionRepository)
//    {
//        _roleRepository = roleRepository;
//        _permissionRepository = permissionRepository;
//    }

//    public void AddRole(string roleName, string description, List<int> permissionIds)
//    {
//        // Create a new role
//        var role = new Role
//        {
//            Name = roleName,
//            Description = description,
//            Active = true,
//            CreatedAt = DateTime.Now,
//            UpdatedAt = DateTime.Now
//        };

//        _roleRepository.AddAsync(role);

//        // Assign permissions to the role
//        foreach (var permissionId in permissionIds)
//        {
//            _permissionRepository.AssignPermissionToRole(role.Id, permissionId);
//        }
//    }

//    public void UpdateRole(int roleId, string roleName, string description, List<int> permissionIds)
//    {
//        var role = _roleRepository.GetById(roleId);
//        if (role == null || !role.Active)
//        {
//            throw new InvalidOperationException("The role does not exist or is inactive.");
//        }

//        // Update the role details
//        role.Name = roleName;
//        role.Description = description;
//        role.UpdatedAt = DateTime.Now;

//        _roleRepository.UpdateAsync(role);

//        // Update assigned permissions
//        _permissionRepository.UpdatePermissionsForRole(roleId, permissionIds);
//    }
//}