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
    public interface IQuestionRepo
    {
        List<Question> GetQuestionList();
        PageResponse<IPagedList<VQuestionPagin>> paginations(QuestionReq filter);
        void addOrUpdateQuestion(Question question);
        void deleteQuestion(Question question);
        Question getOne(long id);
    }

    public class QuestionRepo : IQuestionRepo
    {
        Project3Context _dbContext;
        public QuestionRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void addOrUpdateQuestion(Question question)
        {
            if(question.Id == null)
            {
                _dbContext.Questions.Add(question);
            }
            else
            {
                _dbContext.Questions.Update(question);
            }
            _dbContext.SaveChanges();
        }

        public void deleteQuestion(Question question)
        {
            _dbContext.Questions.Update(question);
            _dbContext.SaveChanges();
        }

        public Question getOne(long id)
        {
            var data = _dbContext.Questions.Where(r => r.Id == id).First();

            return data;
        }

        public List<Question> GetQuestionList()
        {
            return _dbContext.Questions.Where(r => r.IsDelete ==0).ToList();
            
        }

        public PageResponse<IPagedList<VQuestionPagin>> paginations(QuestionReq filter)
        {
            var param = new List<SqlParameter>();
            StringBuilder data = new StringBuilder("select b.Id,b.ExamId,b.NameQuestion,b.CreatedAt from Question as b where b.IsDelete = 0 ");

            if (!string.IsNullOrEmpty(filter.NameQuestion))
            {
                data.Append(" and LOWER(b.NameQuestion) LIKE '%' + @NameQuestion + '%' ");
                param.Add(new SqlParameter("@NameQuestion", SqlDbType.NVarChar) { Value = filter.NameQuestion.ToLower() });
            }
            if (filter.ExamId != null)
            {
                data.Append(" and b.ExamId = @ExamId");
                param.Add(new SqlParameter("@ExamId", SqlDbType.VarChar) { Value = filter.ExamId });
            }
            var query = _dbContext.Set<Question>().FromSqlRaw(data.ToString(), param.ToArray())
                .OrderBy(r => r.NameQuestion).ThenByDescending(r => r.CreatedAt)
                .Select(
                r => new VQuestionPagin
                {
                    Id = r.Id,
                    ExamId = r.ExamId,
                    NameQuestion = r.NameQuestion,
                    CreatedAt = r.CreatedAt
                });

            var total = query.Count();

            var pageData = query.ToPagedList((int)filter.pageNumber, (int)filter.pageSize);

            var pageTotal = Math.Round((decimal)total / (int)filter.pageSize);

            return new PageResponse<IPagedList<VQuestionPagin>>(pageData, (int)filter.pageNumber, (int)filter.pageSize, total, (int)pageTotal);
        }
    }
}
