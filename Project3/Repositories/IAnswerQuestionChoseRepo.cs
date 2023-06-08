using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Migrations;
using Project3.Models;

namespace Project3.Repositories
{
    public interface IAnswerQuestionChoseRepo
    {
        void create(RequestAnswerReq req);
        AnswerQuestionChose getOne(long id);
        int? countAnswerQuestionChose(long historyExamId);
        List<AnswerQuestionChose> getListByExamId(long ExamId);
    }

    public class AnswerQuestionChoseRepo : IAnswerQuestionChoseRepo
    {
        Project3Context _dbContext;

        public AnswerQuestionChoseRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void create(RequestAnswerReq req)
        {
            AnswerQuestionChose question;
            List<AnswerQuestionChose> addRange = new List<AnswerQuestionChose>();
            foreach (DTOQuestionChose dto in req.answer)
            {
                question = new AnswerQuestionChose
                {
                    QuestionId = dto.questionId,
                    AnswerChoseId = dto.answerId,
                    HistoryExamId = dto.examId,
                    Score = dto.score,
                    CreatedAt = DateTime.Now
                };
                addRange.Add(question);
            }
            _dbContext.AnswerQuestionChoses.AddRange(addRange);
            _dbContext.SaveChanges();
        }

        public int? countAnswerQuestionChose(long historyExamId)
        {
            return _dbContext.AnswerQuestionChoses.Where(r => r.HistoryExamId == historyExamId).Count();
        }

        public AnswerQuestionChose getOne(long id)
        {
            return _dbContext.Set<AnswerQuestionChose>().FirstOrDefault(e => e.Id == id);
        }

        public List<AnswerQuestionChose> getListByExamId(long ExamId)
        {
            return _dbContext.AnswerQuestionChoses.Where(r => r.HistoryExamId == ExamId).ToList();
        }
    }
}
