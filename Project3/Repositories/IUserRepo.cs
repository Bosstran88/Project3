using Microsoft.EntityFrameworkCore;
using PagedList;
using Project3.Entity.Response;
using Project3.Migrations;
using Project3.Models;
using System.Text;

namespace Project3.Repositories
{
    public interface IUserRepo
    {
        User getUserByEmail(string email);
        PageResponse<IPagedList<User>> search();
        User findById(string? id);
        string createOrUpdate(User user);
        User getById(string id);

        // Xóa Phần Trên
        bool UserExistsByUsername(string username);
        User getOne(long id);
        void AddUser(User user);
        User getUserByName(string username);
        UserInfoResponse getInfoUser(long id);
        AdminInfoResponse getInfoAdmin(long id);
    }

    public class UserRepo : IUserRepo
    {
        Project3Context _context;
        public UserRepo(Project3Context context) 
        {
            _context = context;
        }

        public string createOrUpdate(User user)
        {
            throw new NotImplementedException();
        }

        public User findById(string? id)
        {
            throw new NotImplementedException();
        }

        public User getById(string id)
        {
            throw new NotImplementedException();
        }

        public User getUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public PageResponse<IPagedList<User>> search()
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.First(u => u.UserName == username);
        }

        public void AddUserAsync(User user)
        {
            _context.Users.AddAsync(user);
            _context.SaveChangesAsync();
        }
        // XÓa Phần trên
        public bool UserExistsByUsername(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }

        public void AddUser(User user)
        {
            _context.Users.AddAsync(user);
            _context.SaveChangesAsync();
        }

        public User getOne(long id)
        {
            return _context.Users.Where(i => i.Id == id).First();
        }

        public User getUserByName(string username)
        {
            return _context.Users.Where(i => i.UserName == username).First();
        }
        public AdminInfoResponse getInfoAdmin(long id)
        {
            return _context.Users.Where(i => i.Id == id)
                .Select(u => new AdminInfoResponse
                {
                    UserName = u.UserName
                }).First();
        }
        public UserInfoResponse getInfoUser(long id)
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
                FullName = query.FullName,
                IdCardStudent = query.IdCardStudent,
                Email = query.Email,
                CreateAt = query.CreatedAt
            };
            
        }
    }
}
