using Project3.Migrations;
using Project3.Models;

namespace Project3.Repositories
{
    public interface IAnswerQuestionRepo
    {
        List<AnswerQuestion> GetAnswerQuestionList();
        void addOrUpdateAnswerQuestion(AnswerQuestion answerQuestion);
        void deleteAnswerQuestion(AnswerQuestion answerQuestion);
        AnswerQuestion getOne(long id);
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
    }
}
