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
        BaseResponse getPagin();
    }

    public class InformationStudentService : IInformationStudentService
    {
        IInformationStudentRepo inforRepo;

        public InformationStudentService(IInformationStudentRepo _inforRepo)
        {
            inforRepo = _inforRepo;
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
            var data = inforRepo.getOne(id);
            return new BaseResponse(new VInfomationStudent
            {
                id = data.Id,
                fullName = data.FullName,
                DateBirth = data.DateBirth,
                idCardStudent = data.IdCardStudent,
                wasBorn = data.WasBorn,
                identityCard = data.IdentityCard,
                startCard = data.StartCard,
                endCard = data.EndCard,
                fromCard = data.FromCard,
                status = data.Status,
                email = data.Email,
                createAt = data.CreatedAt,
                updatedAt = data.UpdateAt,
            });
        }

        public BaseResponse getPagin()
        {
            return new BaseResponse(inforRepo.);
        }
    }
}
