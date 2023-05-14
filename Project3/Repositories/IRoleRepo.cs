using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Migrations;
using Project3.Models;
using Project3.Utils;
using System.Data;
using System.Text;

namespace Project3.Repositories
{
    public interface IRoleRepo
    {
        PageResponse<IPagedList<VRolePagin>> paginations(RoleReq roleReq);
        IEnumerable<VRolePagin> getAllRole();
        void create(Role role);
        void update(Role role);
        Role GetRoleById(long id);
        Role FindByName(string roleName);
        bool exitRoleName(string roleName);
    }

    public class RoleRepo : IRoleRepo
    {
        Project3Context _context;

        public RoleRepo(Project3Context context)
        {
            _context = context;
        }

        public Role FindByName(string roleName)
        {
            return _context.Roles.Where(n => n.NameRole == roleName && n.IsDelete == Constants.IsDelete.False).First();
        }

        public IEnumerable<VRolePagin> getAllRole()
        {
            return _context.Roles.Where(r => r.IsDelete == 0).ToList().Select(r =>
            new VRolePagin
            {
                Id = r.Id,
                NameRole = r.NameRole,
                CreateAt = r.CreatedAt,
                UpdateAt = r.UpdateAt
            });
        }

        public Role GetRoleById(long id)
        {
            return _context.Roles.Where(n => n.Id == id && n.IsDelete == Constants.IsDelete.False).First();

        }

        public PageResponse<IPagedList<VRolePagin>> paginations(RoleReq roleReq)
        {
            var param = new List<SqlParameter>();
            StringBuilder data = new StringBuilder("select rl.Id,rl.NameRole,rl.CreatedAt,rl.UpdateAt from Roles as rl\r\nwhere rl.IsDelete = 0 ");

            if (!string.IsNullOrEmpty(roleReq.RoleName))
            {
                data.Append(" and LOWER(rl.NameRole) LIKE '%' + @roleName + '%' ");
                param.Add(new SqlParameter("@roleName", SqlDbType.NVarChar) { Value = roleReq.RoleName.ToLower() });
            }

            var query = _context.Set<Role>().FromSqlRaw(data.ToString())
                .OrderBy(r => r.NameRole)
                .Select(r => new VRolePagin
                {
                    Id = r.Id,
                    NameRole = r.NameRole,
                    CreateAt = r.CreatedAt,
                    UpdateAt = r.UpdateAt
                });

            var total = query.Count();

            var pageData = query.ToPagedList((int)roleReq.pageNumber, (int)roleReq.pageSize);

            var pageTotal = Math.Round((decimal)total / (int)roleReq.pageSize);

            return new PageResponse<IPagedList<VRolePagin>>(pageData, (int)roleReq.pageNumber, (int)roleReq.pageSize, total, (int)pageTotal);
        }

        public void create(Role role)
        {
            _context.Roles.Add(role);   
            _context.SaveChanges();
        }

        public void update(Role role) 
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public bool exitRoleName(string roleName)
        {
            return _context.Roles.Any(r =>r.NameRole ==  roleName && r.IsDelete == Constants.IsDelete.False);
        }
    }
}
