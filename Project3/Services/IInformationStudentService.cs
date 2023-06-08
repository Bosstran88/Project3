using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;
using Project3.Utils;

namespace Project3.Services
{
    public interface IInformationStudentService
    {
        BaseResponse UpdateInfo(UpdateInformationStudent req);
        BaseResponse getOne(long id);
        BaseResponse getPagin(InfomationStudentReq req);
        BaseResponse deleteAcount(long id);
    }

    public class InformationStudentService : IInformationStudentService
    {
        IInformationStudentRepo inforRepo;
        IUserRepo userRepo;

        public InformationStudentService(IInformationStudentRepo _inforRepo, IUserRepo _userRepo)
        {
            inforRepo = _inforRepo;
            userRepo = _userRepo;
        }

        public BaseResponse deleteAcount(long id)
        {
            var data = inforRepo.getOne(id);
            if(data == null) throw new ValidateException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            data.UpdateAt = DateTime.Now;
            data.IsDelete = Constants.IsDelete.True;
            data.Status = Constants.Status.Delete;
            return new BaseResponse();
        }

        public BaseResponse UpdateInfo(UpdateInformationStudent req)
        {
            InformationStudent student = inforRepo.getOne(req.Id);
            string idcard;
            if (student == null)
            {
                throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            }
            if (!string.Equals(student.Email, req.Email) && !string.IsNullOrEmpty(student.Email))
            {
                if (inforRepo.exitByEmail(req.Email))
                {
                    throw new ValidateException(MESSAGE.VALIDATE.UNQUE_EMAIL);
                }
            }
            student.FullName = req.FullName;
            student.DateBirth = req.DateBirth;
            while (true)
            {
                idcard = Guid.NewGuid().ToString();
                if (!inforRepo.exitIdCardStudent(idcard))
                {
                    break;
                }
            }
            student.Email = req.Email;
            student.IdCardStudent = idcard;
            student.WasBorn = req.WasBorn;
            student.IdentityCard = req.IdentityCard;
            student.StartCard = req.StartCard;
            student.EndCard = req.EndCard;
            student.FromCard = req.FromCard;
            student.IsDelete = Constants.IsDelete.False;
            student.UpdateAt = DateTime.Now;
            student.Status = req.Status;

            inforRepo.UpdateInfo(student);
            return new BaseResponse();
        }

        public BaseResponse getOne(long id)
        { 
            var dataAcc = userRepo.getOne(id);
            var newAcc = new UserRes
            {
                Id = dataAcc.Id,
                UserName = dataAcc.UserName,
                CreatedAt = dataAcc.CreatedAt,
                UpdateAt = dataAcc.UpdateAt
            };
            var dataInfor = inforRepo.getOne(id);
            var newInfo = new VInfomationStudent
            {
                id = dataInfor.Id,
                fullName = dataInfor.FullName,
                DateBirth = dataInfor.DateBirth,
                idCardStudent = dataInfor.IdCardStudent,
                wasBorn = dataInfor.WasBorn,
                identityCard = dataInfor.IdentityCard,
                startCard = dataInfor.StartCard,
                endCard = dataInfor.EndCard,
                fromCard = dataInfor.FromCard,
                status = dataInfor.Status,
                email = dataInfor.Email,
                createAt = dataInfor.CreatedAt,
                updatedAt = dataInfor.UpdateAt,
            };
            return new BaseResponse(new detailAccountStudentResponse
            {
                accountStudent = newAcc,
                infoStudetn = newInfo
            });
        }

        public BaseResponse getPagin(InfomationStudentReq req)
        {
            return new BaseResponse(inforRepo.pagintions(req));
        }
    }
}
