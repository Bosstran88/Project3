using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;
using Project3.Utils;

namespace Project3.Services
{
    public interface IRoleService
    {
        BaseResponse createOrUpdate(AddRoleReq addRole);
        BaseResponse deleteById(long id);
        BaseResponse getOne(long id);
        BaseResponse getPagin(RoleReq roleReq);
    }
    public class RoleService : IRoleService
    {
        IRoleRepo roleRepo;
        public RoleService(IRoleRepo _roleRepo)
        {
            roleRepo = _roleRepo;
        }

        public BaseResponse createOrUpdate(AddRoleReq addRole)
        {
            Role role;
            if (addRole.Id == null)
            {
                if (roleRepo.exitRoleName(addRole.RoleName))
                {
                    throw new ValidateException(MESSAGE.VALIDATE.INPUT_INVALID);
                }
                role = new Role
                {
                    NameRole = addRole.RoleName,
                    IsDelete = Constants.IsDelete.False,
                    CreatedAt = DateTime.Now,
                };
                roleRepo.create(role);
            }
            else
            {
                role = roleRepo.GetRoleById((long)addRole.Id);
                if (role == null)
                {
                    throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
                }
                if (!string.Equals(addRole.RoleName, role.NameRole))
                {
                    if (roleRepo.exitRoleName(addRole.RoleName))
                    {
                        throw new ValidateException(MESSAGE.VALIDATE.INPUT_INVALID);
                    }
                }
                role.NameRole = addRole.RoleName;
                role.UpdateAt = DateTime.Now;
                roleRepo.update(role);
            }
            return new BaseResponse();
        }

        public BaseResponse deleteById(long id)
        {
            var data = roleRepo.GetRoleById(id);
            if (data == null)
            {
                throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            }
            data.IsDelete = 1;
            data.UpdateAt = DateTime.Now;
            roleRepo.update(data);
            return new BaseResponse();
        }

        public BaseResponse getOne(long id)
        {
            var data = roleRepo.GetRoleById(id);
            if (data == null)
            {
                return new BaseResponse();
            }
            return new BaseResponse(new VRoleOne
            {
                Id = data.Id,
                RoleName = data.NameRole
            });
        }

        public BaseResponse getPagin(RoleReq roleReq)
        {
            return new BaseResponse(roleRepo.paginations(roleReq));
        }
    }
}
