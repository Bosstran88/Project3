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
    public class RoleService  : IRoleService
    {
        Role role;
        IRoleRepo roleRepo;
        public RoleService(IRoleRepo _roleRepo) 
        {
            roleRepo = _roleRepo;
        }

        public BaseResponse createOrUpdate(AddRoleReq addRole)
        {
            if(addRole.Id == null)
            {
                this.role = new Role();
                this.role.IsDelete = Constants.IsDelete.False;
                this.role.CreateAt = DateTime.Now;
            }
            else
            {
                this.role = roleRepo.GetRoleById((long)addRole.Id);
                if(this.role == null)
                {
                    throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
                }
                this.role.UpdateAt = DateTime.Now;
            }
            convertFromDtoToModel(addRole);
            roleRepo.createOrUpdate(this.role);
            return new BaseResponse();
        }

        private void convertFromDtoToModel(AddRoleReq addRole)
        {
            this.role.NameRole = addRole.RoleName;
        }

        public BaseResponse deleteById(long id)
        {
            var data = roleRepo.GetRoleById(id);
            if(data == null)
            {
                throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            }
            data.IsDelete = 1;
            data.UpdateAt = DateTime.Now;
            roleRepo.deleteRole(role);
            return new BaseResponse();
        }

        public BaseResponse getOne(long id)
        {
            var data = roleRepo.GetRoleById(id);
            if(data == null)
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
