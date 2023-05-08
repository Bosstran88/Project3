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
        Course course;
        public CourseService(ICourseRepo courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public BaseResponse createOrUpdate(AddCourseReq course)
        {
            if(course.Id == null)
            {
                this.course = new Course();
                this.course.CoursesName = course.CoursesName;
                this.course.TotalTime = course.TotalTime;
            }
            else
            {
                this.course = _courseRepo.getOne((long)course.Id);
                if(this.course == null)
                {
                    throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
                }
                this.course.UpdateAt = DateTime.Now;
            }
            convertFromDtoToModel(course);
            _courseRepo.addOrUpdateCourseRepo(this.course);
            return new BaseResponse();
        }

        private void convertFromDtoToModel(AddCourseReq courses)
        {
            course.Id = (long)courses.Id;
            course.CoursesName = courses.CoursesName;
            course.TotalTime = courses.TotalTime;
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
                CreatedId = data.CreatedId,
                CreatedAt = data.CreatedAt
            };
            return new BaseResponse(format);
        }
    }
}
