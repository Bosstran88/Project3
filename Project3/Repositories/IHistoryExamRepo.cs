using Project3.Entity.Dto;
using Project3.Migrations;
using Project3.Models;

namespace Project3.Repositories
{
    public interface IHistoryExamRepo
    {
        List<HistoryExam> GetHistoryExamList(long userId);
        void addOrUpdate(HistoryExam historyExam);
        HistoryExam getOne(long id);
        HistoryExam findByInformationStudentIdAdnExamId(long studentInfoId, long ExamId);
        HistoryExam startExam(HistoryExam historyExam);
    }
    public class HistoryExamRepo : IHistoryExamRepo
    {
        Project3Context _dbContext;

        public HistoryExamRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void addOrUpdate(HistoryExam historyExam)
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

        public HistoryExam findByInformationStudentIdAdnExamId(long studentInfoId, long ExamId)
        {
            return _dbContext.HistoryExams.Where(r => r.InformationStudentId == studentInfoId 
            && r.ExamId == ExamId).FirstOrDefault();
        }

        public List<HistoryExam> GetHistoryExamList(long userId)
        {
            return _dbContext.HistoryExams.Where(r => r.InformationStudentId == userId).ToList();
        }

        public HistoryExam getOne(long id)
        {
            return _dbContext.HistoryExams.Where(e => e.Id == id).FirstOrDefault();
        }

        public HistoryExam startExam(HistoryExam historyExam)
        {
            _dbContext.HistoryExams.Add(historyExam);
            _dbContext.SaveChanges();
            return historyExam;
        }
    }
}
