using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;
using Project3.Utils;

namespace Project3.Services
{
    public interface ICourseService
    {
        BaseResponse getOne(long id);
        BaseResponse deleteCourse(long id);
        BaseResponse createOrUpdate(AddCourseReq addCourseReq);
    }
    public class CourseService : ICourseService
    {
        ICourseRepo _courseRepo;
        public CourseService(ICourseRepo courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public BaseResponse createOrUpdate(AddCourseReq course)
        {
            Course cour;
            if(course.Id == null)
            {
                if (_courseRepo.exitByNameCourse(course.CoursesName))
                {
                    throw new ValidateException(MESSAGE.VALIDATE.INPUT_INVALID);
                }
                cour = new Course();
                cour.CoursesName = course.CoursesName;
                cour.TotalTime = course.TotalTime;
                cour.CreatedAt = DateTime.Now;
            }
            else
            {
                cour = _courseRepo.getOne((long)course.Id);
                if(cour == null)
                {
                    throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
                }
                if(!string.Equals(cour.CoursesName,course.CoursesName))
                {
                    if (_courseRepo.exitByNameCourse(course.CoursesName))
                    {
                        throw new ValidateException(MESSAGE.VALIDATE.INPUT_INVALID);
                    }
                }
                cour.UpdateAt = DateTime.Now;
            }
           
            _courseRepo.addOrUpdateCourseRepo(cour);
            return new BaseResponse();
        }

        public BaseResponse deleteCourse(long id)
        {
            var data = _courseRepo.getOne(id);
            if(data == null)
            {
                throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            }
            data.UpdateAt = DateTime.Now;
            _courseRepo.deleteCourseRepo(data);
            return new BaseResponse();
        }

        public BaseResponse getOne(long id)
        {
            var data = _courseRepo.getOne(id);
            if (data == null)
            {
                return new BaseResponse();
            }
            var format = new VCourseOne
            {
                Id = data.Id,
                CoursesName = data.CoursesName,
                TotalTime = data.TotalTime,
                CreatedAt = data.CreatedAt
            };
            return new BaseResponse(format);
        }
    }
}
