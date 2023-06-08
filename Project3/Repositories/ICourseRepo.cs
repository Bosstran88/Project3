using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Migrations;
using Project3.Models;
using Project3.Utils;
using System.Data;
using System.Text;

namespace Project3.Repositories
{
    public interface ICourseRepo
    {
        List<Course> GetCourseList();
        void addOrUpdateCourseRepo(Course course);
        void deleteCourseRepo(Course course);
        Course getOne(long id);
        bool exitByNameCourse(string nameCourse);
        PageResponse<IPagedList<VCourseRes>> pagination(CourseSearchReq filter);
        DetailCourse getById(long id);
    }
    public class CourseRepo : ICourseRepo
    {
        Project3Context _dbContext;
        public CourseRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void addOrUpdateCourseRepo(Course course)
        {
            if (course.Id == null)
            {
                _dbContext.Courses.Add(course);
            }
            else
            {
                _dbContext.Courses.Update(course);
            }
            _dbContext.SaveChanges();
        }

        public void deleteCourseRepo(Course course)
        {
            _dbContext.Courses.Update(course);
            _dbContext.SaveChanges();
        }

        public bool exitByNameCourse(string nameCourse)
        {
            return _dbContext.Courses.Any(r => r.CoursesName == nameCourse && r.IsDelete == Constants.IsDelete.False);
        }

        public List<Course> GetCourseList()
        {
            return _dbContext.Courses.Where(r => r.Id == 0).ToList();
        }

        public Course getOne(long id)
        {
            var data = _dbContext.Courses.Where(r => r.Id == id).First();

            return data;
        }

        public DetailCourse getById(long id)
        {
            var data = _dbContext.Courses.Where(r => r.Id == id && r.IsDelete == 0).FirstOrDefault();
            if (data == null)
            {
                return null;
            }
            var subject = _dbContext.Subjects.Where(r => r.CoursesId == id && r.IsDelete == 0).ToList();
            return new DetailCourse
            {
                Id = data.Id,
                CoursesName = data.CoursesName,
                TotalTime = data.TotalTime,
                Level = data.Level,
                CreatedAt = data.CreatedAt,
                UpdateAt = data.UpdateAt,
                Subjects = subject,
            };
        }

        public PageResponse<IPagedList<VCourseRes>> pagination(CourseSearchReq filter)
        {
            var param = new List<SqlParameter>();
            StringBuilder data = new StringBuilder(" select c.Id,c.CoursesName,c.TotalTime,c.IsSale,c.level,c.CreatedAt,c.UpdateAt from Courses as c\r\n where c.IsDelete = 0 ");
            if (!string.IsNullOrEmpty(filter.title))
            {
                data.Append(" and LOWER(c.CoursesName) LIKE '%' + @name + '%' ");
                param.Add(new SqlParameter("@name", SqlDbType.NVarChar) { Value = filter.title.ToLower() });
            }
            if (filter.isSale != null)
            {
                data.Append(" and c.IsSale = @sale ");
                param.Add(new SqlParameter("@sale", SqlDbType.Int) { Value = filter.isSale });
            }
            if (filter.fromTime != null && filter.toTime != null)
            {
                data.Append(" and c.CreatedAt >= @createFrom and c.CreatedAt <= @createTo ");
                param.Add(new SqlParameter("@createFrom", SqlDbType.DateTime) { Value = filter.fromTime });
                param.Add(new SqlParameter("@createTo", SqlDbType.DateTime) { Value = filter.toTime });
            }
            var query = _dbContext.Set<Course>().FromSqlRaw(data.ToString(), param.ToArray()).OrderBy(r => r.CoursesName).ThenByDescending(r => r.CreatedAt)
                .Select(r => new VCourseRes
                {
                    Id = r.Id,
                    CoursesName = r.CoursesName,
                    TotalTime = r.TotalTime,
                    IsSale = r.IsSale,
                    Level = r.Level,
                    CreatedAt = r.CreatedAt,
                    UpdateAt = r.UpdateAt

                });
            var total = query.Count();

            var pageData = query.ToPagedList((int)filter.pageNumber, (int)filter.pageSize);

            var pageTotal = Math.Round((decimal)total / (int)filter.pageSize);

            return new PageResponse<IPagedList<VCourseRes>>(pageData, (int)filter.pageNumber, (int)filter.pageSize, total, (int)pageTotal);
        }

    }
}