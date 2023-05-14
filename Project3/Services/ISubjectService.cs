using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;
using Project3.Utils;

namespace Project3.Services
{
    public interface ISubjectService
    {
        BaseResponse getOne(long id);
        BaseResponse getPagin(SubjectReq filter);
        BaseResponse deleteSubject(long id);
        BaseResponse createOrUpdate(AddSubjectReq subjectReq);
    }
    public class SubjectService : ISubjectService
    {
        ISubjectRepo _subjectRepo;
        Subject subject;

        public SubjectService(ISubjectRepo subjectRepo)
        {
            _subjectRepo = subjectRepo;
        }

        public BaseResponse createOrUpdate(AddSubjectReq subjectReq)
        {
            if(subjectReq.Id == null)
            {
                if (_subjectRepo.exitByNameCSubject(subjectReq.SubjectName))
                {
                    throw new ValidateException(MESSAGE.VALIDATE.INPUT_INVALID);
                }
                this.subject = new Subject();
                this.subject.IsDelete = Constants.IsDelete.False;
                this.subject.CreatedAt = DateTime.Now;
            }
            else
            {
                this.subject = _subjectRepo.getOne((long)subjectReq.Id);
                if(this.subject == null)
                {
                    throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
                }
                if (!string.Equals(this.subject.SubjectName, subjectReq.SubjectName))
                {
                    if (_subjectRepo.exitByNameCSubject(subjectReq.SubjectName))
                    {
                        throw new ValidateException(MESSAGE.VALIDATE.INPUT_INVALID);
                    }
                }
                this.subject.UpdateAt = DateTime.Now;
            }

            convertFromDtoToModel(subjectReq);
            _subjectRepo.addOrUpdateSubjects(this.subject);
            return new BaseResponse();
        }

        private void convertFromDtoToModel(AddSubjectReq subjects)
        {
            subject.Id = (long)subjects.Id;
            subject.SubjectName = subjects.SubjectName;
            subject.TotalTime = subjects.TotalTime;
            subject.Price = subjects.Price;
            subject.CoursesId = subjects.CoursesId;
        }

        public BaseResponse deleteSubject(long id)
        {
            var data = _subjectRepo.getOne(id);
            if(data == null)
            {
                throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            }
            data.IsDelete = 1;
            data.UpdateAt = DateTime.Now;
            _subjectRepo.DeleteSubject(data);
            return new BaseResponse();
        }

        public BaseResponse getOne(long id)
        {
            var data = _subjectRepo.getOne(id);
            if(data == null)
            {
                return new BaseResponse();
            }
            var format = new VSubjectOne
            {
                Id = data.Id,
                SubjectName = data.SubjectName,
                TotalTime = data.TotalTime,
                Price = data.Price,
                CoursesId = data.CoursesId,
                CreatedAt = data.CreatedAt
            };
            return new BaseResponse(format);
        }

        public BaseResponse getPagin(SubjectReq filter)
        {
            var data = _subjectRepo.paginations(filter);
            return new BaseResponse(data);
        }
    }
}
