using Project3.Migrations;
using Project3.Models;

namespace Project3.Repositories
{
    public interface IInformationStudentRepo
    {
        InformationStudent getOne(long id);
    }
    public class InformationStudentRepo : IInformationStudentRepo
    {
        Project3Context _dbContext;
        public InformationStudentRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }
        public InformationStudent getOne(long id)
        {
            var data = _dbContext.InformationStudents.Where(r => r.Id == id).First();

            return data;
        }
    }
}
