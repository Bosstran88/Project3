using Project3.Migrations;
using Project3.Models;
using System.Reflection.Metadata;

namespace Project3.Repositories
{
    public interface IHistoryExamRepo
    {
        List<HistoryExam> GetHistoryExamList();
        void addOrUpdateHistotyExams(HistoryExam historyExam);
        void deleteHistoryExam(HistoryExam historyExam);
        HistoryExam getOne(long id);
    }
    public class HistoryExamRepo : IHistoryExamRepo
    {
        Project3Context _dbContext;

        public HistoryExamRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void addOrUpdateHistotyExams(HistoryExam historyExam)
        {
            if(historyExam.Id == null)
            {
                _dbContext.HistoryExams.Add(historyExam);
            }
            else
            {
                _dbContext.HistoryExams.Update(historyExam);
            }
            _dbContext.SaveChanges();
        }

        public void deleteHistoryExam(HistoryExam historyExam)
        {
            _dbContext.HistoryExams.Update(historyExam);
            _dbContext.SaveChanges();
        }

        public List<HistoryExam> GetHistoryExamList()
        {
            return _dbContext.Set<HistoryExam>().ToList();
        }

        public HistoryExam getOne(long id)
        {
            return _dbContext.Set<HistoryExam>().FirstOrDefault(e => e.Id == id);
        }
    }
}
