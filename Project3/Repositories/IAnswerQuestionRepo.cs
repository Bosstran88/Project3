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
    public interface IAnswerQuestionRepo
    {
        List<AnswerQuestion> GetAnswerQuestionList();
        void addOrUpdateAnswerQuestion(AnswerQuestion answerQuestion);
        void deleteAnswerQuestion(AnswerQuestion answerQuestion);
        AnswerQuestion getOne(long id);
        PageResponse<IPagedList<VAnswerQuestionPagin>> paginations(AnswerQuestionReq filter);
    }

    public class AnswerQuestionRepo : IAnswerQuestionRepo
    {
        Project3Context _dbContext;
        public AnswerQuestionRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void addOrUpdateAnswerQuestion(AnswerQuestion answerQuestion)
        {
           if(answerQuestion == null)
            {
                _dbContext.AnswerQuestions.Add(answerQuestion);
            }
            else
            {
                _dbContext.AnswerQuestions.Update(answerQuestion);
            }
            _dbContext.SaveChanges();
        }

        public void deleteAnswerQuestion(AnswerQuestion answerQuestion)
        {
            _dbContext.AnswerQuestions.Update(answerQuestion);
            _dbContext.SaveChanges();
        }

        public List<AnswerQuestion> GetAnswerQuestionList()
        {
            return _dbContext.AnswerQuestions.Where(r => r.IsDelete == 0).ToList();
        }
        public AnswerQuestion getOne(long id)
        {
                var data = _dbContext.AnswerQuestions.Where(r => r.Id == id).First();
                return data;
        }

        public PageResponse<IPagedList<VAnswerQuestionPagin>> paginations(AnswerQuestionReq filter)
        {
            var param = new List<SqlParameter>();
            StringBuilder data = new StringBuilder("select b.Id,b.AnswerQuestion1,b.Score,b.CreatedAt from AnswerQuestion as b where b.IsDelete = 0 ");

            if (!string.IsNullOrEmpty(filter.AnswerQuestion1))
            {
                data.Append(" and LOWER(b.AnswerQuestion1) LIKE '%' + @answerQuestion1 + '%' ");
                param.Add(new SqlParameter("@answerQuestion", SqlDbType.NVarChar) { Value = filter.AnswerQuestion1.ToLower() });
            }
            if (filter.QuestionId != null)
            {
                data.Append(" and b.QuestionId = @questionId");
                param.Add(new SqlParameter("@questionId", SqlDbType.VarChar) { Value = filter.QuestionId });
            }
            var query = _dbContext.Set<AnswerQuestion>().FromSqlRaw(data.ToString(), param.ToArray())
                .OrderBy(r => r.AnswerQuestion1).ThenByDescending(r => r.CreatedAt)
                .Select(
                r => new VAnswerQuestionPagin
                {
                    Id = r.Id,
                    AnswerQuestion1 = r.AnswerQuestion1,
                    Score = r.Score,
                    CreatedAt = r.CreatedAt
                });

            var total = query.Count();

            var pageData = query.ToPagedList((int)filter.pageNumber, (int)filter.pageSize);

            var pageTotal = Math.Round((decimal)total / (int)filter.pageSize);

            return new PageResponse<IPagedList<VAnswerQuestionPagin>>(pageData, (int)filter.pageNumber, (int)filter.pageSize, total, (int)pageTotal);
        }
    }
}
