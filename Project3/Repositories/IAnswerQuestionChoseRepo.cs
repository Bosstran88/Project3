using Project3.Migrations;
using Project3.Models;
using System.Xml;

namespace Project3.Repositories
{
    public interface IAnswerQuestionChoseRepo
    {
        List<AnswerQuestionChose> GetAnswerQuestionChoseList();
        void createOrUpdateAnswerQuestionChose(AnswerQuestionChose answerQuestionChose);
        void deleteAnswerQuestionChose(AnswerQuestionChose answerQuestionChose);
        AnswerQuestionChose getOne(long id);
    }

    public class AnswerQuestionChoseRepo : IAnswerQuestionChoseRepo
    {
        Project3Context _dbContext;

        public AnswerQuestionChoseRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void createOrUpdateAnswerQuestionChose(AnswerQuestionChose answerQuestionChose)
        {
            _dbContext.AnswerQuestionChoses.Add(answerQuestionChose);
            _dbContext.SaveChanges();
        }

        public void deleteAnswerQuestionChose(AnswerQuestionChose answerQuestionChose)
        {
            _dbContext.AnswerQuestionChoses.Update(answerQuestionChose);
            _dbContext.SaveChanges();
        }

        public List<AnswerQuestionChose> GetAnswerQuestionChoseList()
        {
            return _dbContext.Set<AnswerQuestionChose>().ToList();
        }

        public AnswerQuestionChose getOne(long id)
        {
            return _dbContext.Set<AnswerQuestionChose>().FirstOrDefault(e => e.Id == id);
        }
    }
}
