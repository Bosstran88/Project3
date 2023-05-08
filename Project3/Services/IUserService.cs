using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;
using Project3.Utils;

namespace Project3.Services
{
    public interface IUserService
    {
        BaseResponse register(RegisterReq registerUser);
        BaseResponse login(LoginReq loginUser);
        BaseResponse getInfo(AuthenReq authen);
    }

    public class UserService : IUserService
    {
        User user;
        Role role;
        IInformationStudentRepo studentRepo;
        IRoleRepo roleRepo;
        IUserRepo userRepo;
        IUserRoleRepo userRoleRepo;
        ISecurityService securityService;
        InformationStudent student;

        public UserService(IUserRoleRepo _userRoleRepo,IRoleRepo _roleRepo, IUserRepo _userRepo, ISecurityService _securityService, IInformationStudentRepo _studentRepo)
        {
            roleRepo = _roleRepo;
            userRepo = _userRepo;
            userRoleRepo = _userRoleRepo;
            securityService = _securityService;
            studentRepo = _studentRepo;
        }

        public BaseResponse getInfo(AuthenReq authen)
        {
            var user = userRepo.getById(authen.Id);
            if(user == null)
            {
                return new BaseResponse(MESSAGE.STATUS_RESPONSE.UNAUTHORIZED, MESSAGE.VALIDATE.USER_NOT_FOUND);
            }
            if(authen.RoleName == MESSAGE.VALIDATE.ROLE_USER)
            {
                var data = userRepo.getInfoUser(authen.Id);
                return new BaseResponse(data);
            }
            else
            {
                var data = userRepo.getInfoAdmin(authen.Id);
                return new BaseResponse(data);
            }
        }

        public BaseResponse login(LoginReq loginUser)
        {
            if (string.IsNullOrEmpty(loginUser.Username))
            {
                throw new AuthenException();
            }
            if(string.IsNullOrEmpty(loginUser.Password))
            {
                throw new AuthenException();
            }
            this.user = null;
            this.user = userRepo.getUserByName(loginUser.Username);
            if(user == null)
            {
                throw new AuthenException();
            }
            var userRole = userRoleRepo.GetUserRoleById(this.user.Id); 
            if(userRole == null)
            {
                throw new AuthenException();
            }
            var role = roleRepo.GetRoleById((long)userRole.RoleId);
            if(role == null)
            {
                throw new AuthenException();
            }
            bool checkPass = securityService.verifyPasswordHash(loginUser.Password , this.user.PasswordHash, this.user.PasswordSalt);
            if(checkPass)
            {
                var token = securityService.createToken(this.user, role.NameRole);
                DateTime today = DateTime.Now;
                return new BaseResponse(new ResponseTokenDto(token, today, today.AddDays(1)));
            }
            throw new AuthenException();
        }

        public BaseResponse register(RegisterReq registerUser)
        {
            if (userRepo.UserExistsByUsername(registerUser.Username))
            {
                throw new ValidateException(MESSAGE.VALIDATE.REGISTER_USERNAME);
            }
            this.user = new User();
            var pass = securityService.createPasswordHash(registerUser.Password);
            this.user.UserName = registerUser.Username;
            this.user.PasswordHash = pass.PasswordHash;
            this.user.PasswordSalt = pass.PasswordSalt;
            this.user.CreatedAt = DateTime.Now;
            this.user.IsDelete = Constants.IsDelete.False;
            if (!string.IsNullOrEmpty(registerUser.role))
            {
                this.role = roleRepo.FindByName(registerUser.role);
                if (this.role == null)
                {
                    throw new ValidateException(MESSAGE.VALIDATE.ROLE_NOT_FOUND);
                }

                userRepo.AddUser(this.user);
            }
            else
            {
                this.role = roleRepo.FindByName(MESSAGE.VALIDATE.ROLE_USER);
                if (this.role == null)
                {
                    throw new ValidateException(MESSAGE.VALIDATE.ROLE_NOT_FOUND);
                }
                this.user.UserRoles = new List<UserRole>(){
                new UserRole()
                {
                    Role = this.role
                }};
                userRepo.AddUser(this.user);
                this.student = new InformationStudent()
                {
                    Id = this.user.Id
                };
                studentRepo.AddInfo(this.student);
            }
            return new BaseResponse(MESSAGE.STATUS_RESPONSE.SUCCESS, MESSAGE.VALIDATE.REGISTER_SUCCESS);
        }
    }
}
