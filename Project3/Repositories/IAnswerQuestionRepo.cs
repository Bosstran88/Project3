using Microsoft.Data.SqlClient;
using PagedList;
using Project3.Entity.Dto;
using Project3.Entity.Response;
using Project3.Migrations;
using Project3.Models;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Project3.Utils;

namespace Project3.Repositories
{
    public interface IAnswerQuestionRepo
    {        
        void addOrUpdateAnswerQuestion(AnswerQuestion answerQuestion);
        void deleteAnswerQuestion(AnswerQuestion answerQuestion);
        AnswerQuestion getOne(long id);
        PageResponse<IPagedList<VAnswerQuestionRes>> pagination(AnswerQuestionPageReq answerQuestion);
        int countAnswerOfQuestion(long idQuestion);

        List<AnswerQuestion> getList(long id);
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

        public int countAnswerOfQuestion(long idQuestion) { 
            return _dbContext.AnswerQuestions.Where(r => r.QuestionId == idQuestion).Count();
        }

        public ResponseQuestionListAnswer GetAnswerQuestionList(long idQuestion,long Examid)
        {
            var data = _dbContext.Questions.Where(r => r.IsDelete == 0 && r.Id == idQuestion && r.ExamId == Examid).FirstOrDefault();
            if(data == null){ throw new ValidateException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND); }
            List<VAnswerQuestionDto> answers = _dbContext.AnswerQuestions.Where(r => r.QuestionId == idQuestion).Select(r => new VAnswerQuestionDto
            {
                id = r.Id,
                answer = r.Answer
            }).ToList();
            if(answers.Count == 0) { throw new ValidateException(MESSAGE.VALIDATE.ANSWER_QUESTION_NOT_FOUNT); }
            return new ResponseQuestionListAnswer
            {
                questionId = data.Id,
                nameQuestion = data.NameQuestion,
                listAnswer = answers
            };
        }

        public AnswerQuestion getOne(long id)
        {
            return _dbContext.AnswerQuestions.Where(r => r.Id == id).First();
        }

        public PageResponse<IPagedList<VAnswerQuestionRes>> pagination(AnswerQuestionPageReq answerQuestion) 
        {
            var param = new List<SqlParameter>();
            var data = new StringBuilder(" select c.Id,c.QuestionId,c.AnswerQuestion,c.Score,c.IsDelete,c.CreatedAt,c.UpdateAt from AnswerQuestion as c where c.IsDelete = 0 ");
            if(answerQuestion.questionId != null)
            {
                data.Append(" and c.QuestionId = @questionId ");
                param.Add(new SqlParameter("@questionId", SqlDbType.BigInt) { Value = answerQuestion.questionId});
            }
            if(!string.IsNullOrEmpty(answerQuestion.nameAnswer)) 
            {
                data.Append(" and LOWER(c.AnswerQuestion) LIKE '%' + @answer + '%' ");
                param.Add(new SqlParameter("@answer" , SqlDbType.NVarChar ) { Value = answerQuestion.nameAnswer });
            }

            var query = _dbContext.Set<AnswerQuestion>().FromSqlRaw(data.ToString(), param.ToArray()).OrderBy(r => r.Answer).ThenByDescending(r => r.CreatedAt).Select(r => new VAnswerQuestionRes
            {
                Id = r.Id,
                QuestionId = r.QuestionId,
                AnswerQuestion = r.Answer,
                Score = r.Score,
                IsDelete = r.IsDelete,
                CreatedAt = r.CreatedAt,
                UpdateAt = r.UpdateAt
            });

            var total = query.Count();
            var pageData = query.ToPagedList((int)answerQuestion.pageNumber, (int)answerQuestion.pageSize);
            var pageTotal = Math.Round((decimal)total / (int)answerQuestion.pageSize);

            return new PageResponse<IPagedList<VAnswerQuestionRes>>(pageData, (int)answerQuestion.pageNumber, (int)answerQuestion.pageSize , total , (int) pageTotal);
        }

        public List<AnswerQuestion> getList(long id)
        {
            var data = _dbContext.AnswerQuestions.Where(r => r.QuestionId == id && r.IsDelete == Constants.IsDelete.False).ToList();
            if(data == null)
            {
                throw new Exception(MESSAGE.VALIDATE.OBJECT_NULL);
            }
            return data;
        }
    }
}
