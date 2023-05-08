using Project3.Entity.Response;
using Project3.Repositories;

namespace Project3.Services
{
    public interface IInformationStudentService
    {
        BaseResponse UpdateInfo();
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

        public BaseResponse UpdateInfo()
        {
            return new BaseResponse();
        }

        public BaseResponse getOne(long id)
        {
            throw new NotImplementedException();
        }

        public BaseResponse getPagin()
        {
            throw new NotImplementedException();
        }
    }
}
