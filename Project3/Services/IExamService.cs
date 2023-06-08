using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;
using Project3.Utils;

namespace Project3.Services
{
    public interface IExamService
    {
        BaseResponse getOne(long id);
        BaseResponse deleteExam(long id);
        BaseResponse createOrUpdate(AddExamReq examReq);
        BaseResponse getPagin(ExamReq filter);
        BaseResponse getList(ListExamReq filter);
    }

    public class ExamService : IExamService
    {
        IExamRepo _examRepo;
        Exam exam;

        public ExamService(IExamRepo examRepo)
        {
            _examRepo = examRepo;
        }

        public BaseResponse createOrUpdate(AddExamReq examReq)
        {
            if(examReq.Id == null)
            {
                this.exam = new Exam();
                this.exam.IsDelete = Constants.IsDelete.False;
                this.exam.CreatedAt = DateTime.Now;
            }
            else
            {
                this.exam = _examRepo.getOne((long)examReq.Id);
                if(this.exam == null)
                {
                    throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
                }
                this.exam.UpdateAt = DateTime.Now;
            }
            convertFromDtoToModel(examReq);
            _examRepo.addOrUpdateExams(this.exam);
            return new BaseResponse();
        }

        private void convertFromDtoToModel(AddExamReq exams)
        {
            exam.NameExam = exams.NameExam;
            exam.LimitTime = exams.LimitTime;
        }

        public BaseResponse deleteExam(long id)
        {
            var data = _examRepo.getOne(id);
            if (data == null) throw new ValidateException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            data.IsDelete = Constants.IsDelete.True;
            data.UpdateAt = DateTime.Now;
            _examRepo.deleteExam(data);
            return new BaseResponse(); ;
        }

        public BaseResponse getOne(long id)
        {
            var data = _examRepo.getOne(id);
            return new BaseResponse(new VExamOne
            {
                Id = data.Id,
                NameExam = data.NameExam,
                LimitTime = data.LimitTime, 
                CreateAt = data.CreatedAt,
                UpdateAt = data.UpdateAt
            });
        }

        public BaseResponse getPagin(ExamReq filter)
        {
            var data = _examRepo.paginations(filter);
            return new BaseResponse(data);
        }

        public BaseResponse getList(ListExamReq filter)
        {
            return new BaseResponse(_examRepo.getExamList(filter));

        }
    }
}
