using Microsoft.VisualBasic;
using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;
using Project3.Utils;

namespace Project3.Services
{
    public interface ITestFirstService
    {
        BaseResponse getOne(long id);
        BaseResponse deleteTestFirst(long id);
        BaseResponse createOrUpdate(AddTestFirstReq testFirstReq);
    }
    public class TestFirstService : ITestFirstService
    {
        ITestFirstRepo _testFirstRepo;
        TestFirst testFirst;

        public TestFirstService(ITestFirstRepo testFirstRepo)
        {
            _testFirstRepo = testFirstRepo;
        }

        public BaseResponse createOrUpdate(AddTestFirstReq testFirstReq)
        {
            if(testFirstReq.Id == null)
            {
                this.testFirst = new TestFirst();
                this.testFirst.CreatedAt = DateTime.Now;
            }
            else
            {
                this.testFirst = _testFirstRepo.getOne((long)testFirstReq.Id);
                if(this.testFirst == null)
                {
                    throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
                }
            }
            convertFromDtoToModel(testFirstReq);
            _testFirstRepo.addOrUpdateTestFirst(this.testFirst);
            return new BaseResponse();
        }

        private void convertFromDtoToModel(AddTestFirstReq testFirsts)
        {
            testFirst.NameTest = testFirsts.NameTest;
            testFirst.ScoreTest = testFirsts.ScoreTest;
            testFirst.CreatedAt = testFirsts.CreatedAt;
        }

        public BaseResponse deleteTestFirst(long id)
        {
            var data = _testFirstRepo.getOne(id);
            if(data == null)
            {
                throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            }
            _testFirstRepo.deleteTestFirst(data);
            return new BaseResponse();
        }

        public BaseResponse getOne(long id)
        {
            var data = _testFirstRepo.getOne(id);
            if(data == null)
            {
                return new BaseResponse();
            }
            var format = new VTestFirstOne
            {
                Id = data.Id,
                NameTest = data.NameTest,
                ScoreTest = data.ScoreTest,
                CreatedAt = data.CreatedAt,
                InfoStudentId = data.InfoStudentId
            };
            return new BaseResponse(format);
        }
    }
}
