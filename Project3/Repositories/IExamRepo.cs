using PagedList;
using Project3.Entity.Response;
using Project3.Migrations;
using Project3.Models;

namespace Project3.Repositories
{
    public interface IExamRepo
    {
        List<Exam> getExamList();
        void addOrUpdateExams(Exam exam);
        void deleteExam(Exam exam);
        void getOne(long id);
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

        public List<Exam> getExamList()
        {
            return _dbContext.Exams.Where(r => r.IsDelete == 0).ToList();
        }

        public void getOne(long id)
        {
            var data = _dbContext.Exams.Where(r => r.Id == id).First();
        }
    }
}
