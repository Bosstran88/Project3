using Project3.Migrations;
using Project3.Models;

namespace Project3.Repositories
{
    public interface IQuestionRepo
    {
        List<Question> GetQuestionList();
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
    }
}
