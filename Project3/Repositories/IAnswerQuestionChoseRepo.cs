using Project3.Migrations;
using Project3.Models;
using System.Xml;

namespace Project3.Repositories
{
    public interface IAnswerQuestionChoseRepo
    {
        void create(List<AnswerQuestionChose> answerQuestionChose);
        AnswerQuestionChose getOne(long id);
    }

    public class AnswerQuestionChoseRepo : IAnswerQuestionChoseRepo
    {
        Project3Context _dbContext;

        public AnswerQuestionChoseRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void create(List<AnswerQuestionChose> answerQuestionChose)
        {
            _dbContext.AnswerQuestionChoses.AddRange(answerQuestionChose);
            _dbContext.SaveChanges();
        }

        public AnswerQuestionChose getOne(long id)
        {
            return _dbContext.Set<AnswerQuestionChose>().FirstOrDefault(e => e.Id == id);
        }
    }
}
