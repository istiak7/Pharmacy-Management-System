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

namespace Pharmacy_Management_System.Service.Services.Roles
{
    public class RoleCommandService : IRoleCommandService
    {
        private readonly IRoleCommandRepository _roleCommandRepository;
        private readonly IPermissionCommandRepository _permissionCommandRepository;
        public RoleCommandService(IRoleCommandRepository roleCommandRepository, IPermissionCommandRepository permissionCommandRepository) 
        {
            _roleCommandRepository = roleCommandRepository;
            _permissionCommandRepository = permissionCommandRepository;

        }
        #region Command
        public async Task<Result> CreateRole(RoleCreateDto model, bool saveChnages = true)
        {
            if (await CheckIsNameExist(model.Name) is not null) 
            {
                return Utility.GetAlreadyExistMsg("Role Name Already Exist");
            }
            
            var RoleDetails = Role.Create(model.Name, model.Description);

            if (model.PermissionIds is not null && model.PermissionIds.Count > 0) 
            {
                var permissons = await _permissionCommandRepository.FindAllAsync(p =>  model.PermissionIds.Contains(p.Id));

                foreach (var permission in permissons)
                {
                    RoleDetails.Permissions.Add(permission);
                }
            }

            await _roleCommandRepository.InsertAsync(RoleDetails, saveChnages);

            return Utility.GetSuccessMsg(CommonMessages.SavedSuccessfully);
        }

        public async Task<Result> UpdateRole(RoleUpdateDto model, bool saveChnages = true)
        {
            var existingData = await _roleCommandRepository.FindAsync(x => x.Id == model.Id && x.IsActive != (int)StatusId.Delete,
                                                            includeProperties:r =>r.Permissions);

            if (existingData is null)
            {
                return Utility.GetNoDataFoundMsg(CommonMessages.NoDataFound);
            }
           
            existingData.Update(model.Name, model.Description);

            existingData.Permissions.Clear();

            var permissions = await _permissionCommandRepository.FindAllAsync(p => model.PermissionIds.Contains(p.Id));

            foreach (var permission in permissions) 
            { 
                existingData.Permissions.Add(permission);
            }

            await _roleCommandRepository.UpdateAsync(existingData, saveChnages);

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
