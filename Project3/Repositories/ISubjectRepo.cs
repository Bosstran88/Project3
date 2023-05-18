
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
    public interface ISubjectRepo
    {
        List<Subject> GetSubjectList();
        PageResponse<IPagedList<VSubjectPagin>> paginations(SubjectReq filter);
        void addOrUpdateSubjects(Subject subject);
        void DeleteSubject(Subject subject);
        Subject getOne(long id);
        bool exitByNameCSubject(string nameSubject);
    }
    public class SubjectRepo : ISubjectRepo
    {
        Project3Context _dbContext;
        public SubjectRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void addOrUpdateSubjects(Subject subject)
        {
            if (subject.Id == null)
            {
                _dbContext.Subjects.Add(subject);
            }
            else
            {
                _dbContext.Subjects.Update(subject);
            }
            _dbContext.SaveChanges();
        }

        public void DeleteSubject(Subject subject)
        {
            _dbContext.Subjects.Update(subject);
            _dbContext.SaveChanges();
        }

        public bool exitByNameCSubject(string nameSubject)
        {
            return _dbContext.Subjects.Any(r => r.SubjectName == nameSubject && r.IsDelete == Constants.IsDelete.False);
        }

        public Subject getOne(long id)
        {
            var data = _dbContext.Subjects.Where(r => r.Id == id).First();

            return data;
        }

        public List<Subject> GetSubjectList()
        {
            return _dbContext.Subjects.Where(r => r.IsDelete == 0).ToList();
        }

        public PageResponse<IPagedList<VSubjectPagin>> paginations(SubjectReq filter)
        {

            var param = new List<SqlParameter>();
            StringBuilder data = new StringBuilder("select b.Id,b.SubjectName,b.CoureId,b.CreatedAt from Subjects as b\r\nwhere b.IsDelete = 0 ");

            if (!string.IsNullOrEmpty(filter.subjectName))
            {

                data.Append(" and LOWER(b.subjectName) LIKE '%' + LOWER(@subjectName) + '%' OR b.SubjectName = '' ");
                param.Add(new SqlParameter("title", SqlDbType.NVarChar) { Value = filter.subjectName });
            }
            if (filter.courseId != null)
            {

                data.Append(" and b.CourseId = @courseId");
                param.Add(new SqlParameter("@courseId", SqlDbType.VarChar) { Value = filter.courseId });
            }
            var query = _dbContext.Set<Subject>().FromSqlRaw(data.ToString(), param.ToArray())
                .OrderBy(r => r.SubjectName).ThenByDescending(r => r.CreatedAt)
                .Select(
                r => new VSubjectPagin
                {
                    Id = r.Id,
                    SubjectName = r.SubjectName,
                    Price = r.Price,
                    CoursesId = r.CoursesId,
                    CreatedAt = r.CreatedAt
                });
            
            var total = query.Count();
            var pageData = query.ToPagedList((int)filter.pageNumber, (int)filter.pageSize);

            var pageTotal = Math.Round((decimal)total / (int)filter.pageSize);

            return new PageResponse<IPagedList<VSubjectPagin>>(pageData, (int)filter.pageNumber, (int)filter.pageSize, total, (int)pageTotal);
        }
    }
}
