using Project3.Migrations;
using Project3.Models;

namespace Project3.Repositories
{
    public interface IHistoryExamRepo
    {
        List<HistoryExam> GetHistoryExamList(long userId);
        void add(HistoryExam historyExam);
        HistoryExam getOne(long id);
    }
    public class HistoryExamRepo : IHistoryExamRepo
    {
        Project3Context _dbContext;

        public HistoryExamRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void add(HistoryExam historyExam)
        {
            _dbContext.HistoryExams.Add(historyExam);
            _dbContext.SaveChanges();
        }

        public List<HistoryExam> GetHistoryExamList(long userId)
        {
            return _dbContext.HistoryExams.Where(r => r.InfostudentId == userId).ToList();
        }

        public HistoryExam getOne(long id)
        {
            return _dbContext.HistoryExams.FirstOrDefault(e => e.Id == id);
        }
    }
}
