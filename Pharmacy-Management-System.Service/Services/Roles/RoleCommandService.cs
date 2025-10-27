using Microsoft.IdentityModel.Tokens;
using Pharmacy_Management_System.Application.Features.Roles.Commands.Dtos;
using Pharmacy_Management_System.Application.RepositoryInterfaces.Roles;
using Pharmacy_Management_System.Application.ServiceInterfaces.Roles;
using Pharmacy_Management_System.Domain.Entities.Roles;
using Pharmacy_Management_System.Application.Constants;
using Pharmacy_Management_System.Application.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pharmacy_Management_System.Domain.Common.EntityConstant;
using Utility = Pharmacy_Management_System.Application.Common.Utilities.Utility;
using Pharmacy_Management_System.Application.RepositoryInterfaces.Permissions;
using Pharmacy_Management_System.Domain.Contexts;
using Pharmacy_Management_System.Data.DbContexts;

namespace Pharmacy_Management_System.Service.Services.Roles
{
    public class RoleCommandService : IRoleCommandService
    {
        private readonly IRoleCommandRepository _roleCommandRepository;
        private readonly IPermissionCommandRepository _permissionCommandRepository;
        private readonly ApplicationDbContextWrite _dbContext;
        public RoleCommandService(IRoleCommandRepository roleCommandRepository, IPermissionCommandRepository permissionCommandRepository,
                                       ApplicationDbContextWrite dbContext) 
        {
            _roleCommandRepository = roleCommandRepository;
            _permissionCommandRepository = permissionCommandRepository;
            _dbContext = dbContext;

        }
        #region Command
        public async Task<Result> CreateRole(RoleCreateDto model, bool saveChnages = true)
        {
            if (await CheckIsNameExist(model.Name) is not null) 
            {
                return Utility.GetAlreadyExistMsg("Role Name Already Exist");
            }
            
            Role ? RoleDetails = Role.Create(model.Name, model.Description);

            if (model.PermissionIds is not null && model.PermissionIds.Count > 0) 
            {
                var permissons = await _permissionCommandRepository.FindAllAsync(p =>  model.PermissionIds.Contains(p.Id));

                foreach (var permission in permissons)
                {
                    var rolePermission = new RolePermission
                    {
                        RoleId = RoleDetails.Id,
                        PermissionId = permission.Id
                    };
                    RoleDetails.RolePermissions.Add(rolePermission);
                }
            }

            await _roleCommandRepository.InsertAsync(RoleDetails, saveChnages);

            return Utility.GetSuccessMsg(CommonMessages.SavedSuccessfully);
        }

        public async Task<Result> UpdateRole(RoleUpdateDto model, bool saveChnages = true)
        {
            var existingRole = await _roleCommandRepository
                                                     .FindAsync(x => x.Id == model.Id && x.IsActive != (int)StatusId.Delete,
                                                      includeProperties:r =>r.RolePermissions);

            if (existingRole is null)
            {
                return Utility.GetNoDataFoundMsg(CommonMessages.NoDataFound);
            }

            existingRole.Update(model.Name, model.Description);

            var existingPermissionIds = existingRole.RolePermissions
                                                     .Where(x => x.IsActive == (int)StatusId.Active)
                                                     .Select(x => x.PermissionId).ToList();

            var toAdd = model.PermissionIds.Except(existingPermissionIds).ToList();
            var toRemove = existingPermissionIds.Except(model.PermissionIds).ToList();

            var DeactivateData = _dbContext.RolePermissions
                                        .Where(x => toRemove.Contains(x.PermissionId));
                                        
            foreach (var Dd in DeactivateData)
            {
                Dd.IsActive = (int)StatusId.Delete;
                Dd.UpdatedAt = CommonMethods.GetBDCurrentTime();
            }

            foreach(var newPermissionIdAdd  in toAdd)
            {
                var newRolePermission = new RolePermission
                {
                    RoleId = newPermissionIdAdd,
                    PermissionId = newPermissionIdAdd,
                    IsActive = (int)StatusId.Active,
                    UpdatedAt = CommonMethods.GetBDCurrentTime()
                };
                existingRole.RolePermissions.Add(newRolePermission);
            }
       

           

            await _roleCommandRepository.UpdateAsync(existingRole, saveChnages);

            return Utility.GetSuccessMsg(CommonMessages.UpdatedSuccessfully);
        }

        #endregion

        #region Private Method

        private async Task<Role?> CheckIsNameExist(string name)
        {
            return await _roleCommandRepository.FindAsync(x => x.Name == name 
                                                            && x.IsActive != (int)StatusId.Delete);
        }

        #endregion
    }
}
