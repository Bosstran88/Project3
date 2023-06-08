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
    public interface IUserRepo
    {
        bool UserExistsByUsername(string username);
        User getOne(long id);
        void AddUser(User user);
        User getUserByName(string username);
        UserInfoResponse getInfoUser(long id);
        AdminInfoResponse getInfoAdmin(long id);
        void registerUser(User user, InformationStudent student);
        PageResponse<UserRes> pagin(UserPaginReq res);

    }

    public class UserRepo : IUserRepo
    {
        Project3Context _context;
        public UserRepo(Project3Context context)
        {
            _context = context;
        }

        public void registerUser(User user, InformationStudent info)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    info.Id = user.Id;
                    _context.InformationStudents.Add(info);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi xảy ra, rollback transaction
                    transaction.Rollback();
                    throw ex;
                }
            }
        }


        public bool UserExistsByUsername(string username)
        {
            return _context.Users.Any(u => u.UserName == username && u.IsDelete == Constants.IsDelete.False);
        }

        public void AddUser(User user)
        {
            _context.Users.AddAsync(user);
            _context.SaveChangesAsync();
        }

        public User getOne(long id)
        {
            var data = _context.Users.Where(i => i.Id == id && i.IsDelete == Constants.IsDelete.False).FirstOrDefault();
            return data;
        }

        public User? getUserByName(string username)
        {
            var data = _context.Users.Where(i => i.UserName == username && i.IsDelete == Constants.IsDelete.False).FirstOrDefault();
            return data;
        }
        public AdminInfoResponse? getInfoAdmin(long id)
        {
            return _context.Users.Where(i => i.Id == id && i.IsDelete == Constants.IsDelete.False)
                .Select(u => new AdminInfoResponse
                {
                    id = u.Id,
                    userName = u.UserName
                }).FirstOrDefault();
        }
        public UserInfoResponse? getInfoUser(long id)
        {
            var data = from u in _context.Users
                       join st in _context.InformationStudents on u.Id equals st.Id
                       where u.Id == id
                       select new
                       {
                           st.FullName,
                           st.IdCardStudent,
                           st.Email,
                           st.CreatedAt
                       };

            var query = data.First();
            return new UserInfoResponse
            {
                fullName = query.FullName,
                idCardStudent = query.IdCardStudent,
                email = query.Email,
                createAt = query.CreatedAt
            };

        }

        public PageResponse<UserRes> pagin(UserPaginReq res)
        {
            var data = _context.Users
                .Where(r => r.UserName.ToLower().Contains(res.name.ToLower()))
           .Skip((res.pageNumber - 1) * res.pageSize).Take(res.pageNumber * res.pageSize)
           .Select(r => new UserRes
           {
               Id = r.Id,
               UserName = r.UserName,
               CreatedAt = r.CreatedAt,
               UpdateAt = r.UpdateAt
           })
         .ToList();
            if (data == null)
            {
                return new PageResponse<UserRes>();
            }
            var totalRecord = data.Count();
            if (totalRecord < 0) return new PageResponse<UserRes>();
            var totalPage = (int)Math.Ceiling((double)totalRecord / res.pageSize);

            return new PageResponse<UserRes>(data, res.pageNumber, res.pageSize, totalRecord, totalPage);
        }

    }
}
