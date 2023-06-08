using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Migrations;
using Project3.Models;
using System.Data;
using System.Text;

namespace Project3.Repositories
{
    public interface IExamRepo
    {
        List<VExam> getExamList(ListExamReq filter);
        void addOrUpdateExams(Exam exam);
        void deleteExam(Exam exam);
        Exam getOne(long id);
        PageResponse<IPagedList<VExamPagin>> paginations(ExamReq filter);
    }
    public class ExamRepo : IExamRepo
    {
        Project3Context _dbContext;
        public ExamRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void addOrUpdateExams(Exam exam)
        {
            if(exam == null)
            {
                _dbContext.Exams.Add(exam);
            }
            else
            {
                _dbContext.Exams.Update(exam);
            }
            _dbContext.SaveChanges();
        }

        public void deleteExam(Exam exam)
        {
            _dbContext.Exams.Update(exam);
            _dbContext.SaveChanges();
        }

        public List<VExam> getExamList(ListExamReq filter)
        {
            var data = _dbContext.Exams.Where(r => r.IsDelete == 0);
            if (!string.IsNullOrEmpty(filter.NameExam))
            {
                data.Where(r => r.NameExam.ToLower().Contains(filter.NameExam.ToLower()));
            }
            var res = data.Select(r => new VExam
            {
                Id = r.Id,
                NameExam = r.NameExam,
                LimitTime = r.LimitTime,
                CreatedAt = r.CreatedAt
            }).ToList();
            return res;
        }

        public Exam getOne(long id)
        {
            var data = _dbContext.Exams.Where(r => r.Id == id).First();
            return data;
        }

        public PageResponse<IPagedList<VExamPagin>> paginations(ExamReq filter)
        {
            var param = new List<SqlParameter>();
            StringBuilder data = new StringBuilder("select b.Id,b.NameExam,b.LimitTime,b.CreatedAt from Exam as b\r\nwhere b.IsDelete = 0");

            if (!string.IsNullOrEmpty(filter.NameExam))
            {
                data.Append(" and LOWER(b.NameExam) LIKE '%' + LOWER(@nameExam) + '%' OR b.NameExam = '' ");
                param.Add(new SqlParameter("nameExam", SqlDbType.NVarChar) { Value = filter.NameExam });
            }
            var query = _dbContext.Set<Exam>().FromSqlRaw(data.ToString(), param.ToArray())
                .OrderBy(r => r.NameExam).ThenByDescending(r => r.CreatedAt)
                .Select(
                r => new VExamPagin
                {
                    Id = r.Id,
                    NameExam = r.NameExam,
                    LimitTime = r.LimitTime,
                    CreatedAt = r.CreatedAt,
                });

            var total = query.Count();

            var pageData = query.ToPagedList((int)filter.pageNumber, (int)filter.pageSize);

            var pageTotal = Math.Round((decimal)total / (int)filter.pageSize);

            return new PageResponse<IPagedList<VExamPagin>>(pageData, (int)filter.pageNumber, (int)filter.pageSize, total, (int)pageTotal);
        }
    }
}
