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
        void addOrUpdateQuestion(Question question);
        void deleteQuestion(Question question);
        Question getOne(long id);

        PageResponse<IPagedList<VQuestionRes>> pagination(QuestionReq req);
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

        public PageResponse<IPagedList<VQuestionRes>> pagination(QuestionReq req)
        {
            var param = new List<SqlParameter>();
            StringBuilder data = new StringBuilder(" select q.Id , q.NameQuestion , q.CreatedAt , q.UpdateAt , q.ExamId from Question as q where q.IsDelete = 0 ");

            if (!string.IsNullOrEmpty(req.NameQuestion))
            {
                data.Append(" and LOWER(rl.NameRole) LIKE '%' + @roleName + '%' ");
                param.Add(new SqlParameter("@roleName", SqlDbType.NVarChar) { Value = req.NameQuestion.ToLower() });
            }

            var query = _dbContext.Set<Question>().FromSqlRaw(data.ToString())
                .OrderBy(r => r.NameQuestion).ThenByDescending(r => r.CreatedAt)
                .Select(r => new VQuestionRes
                {
                    id = r.Id,
                    nameQuestion = r.NameQuestion,
                    createAt = r.CreatedAt,
                    updateAt = r.UpdateAt,
                    ExamId = r.ExamId
                });

            var total = query.Count();

            var pageData = query.ToPagedList((int)req.pageNumber, (int)req.pageSize);

            var pageTotal = Math.Round((decimal)total / (int)req.pageSize);

            return new PageResponse<IPagedList<VQuestionRes>>(pageData, (int)req.pageNumber, (int)req.pageSize, total, (int)pageTotal);
        }
    }
}
