using Microsoft.Data.SqlClient;
using PagedList;
using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Migrations;
using Project3.Models;
using Project3.Utils;
using System.Text;
using System.Data;

namespace Project3.Repositories
{
    public interface IInformationStudentRepo
    {
        InformationStudent getOne(long id);
        bool exitByEmail(string email);
        bool exitIdCardStudent(string idCardStudent);

        //Xóa cái AddOrUpdate đi
        void AddInfo(InformationStudent informationStudent);
        void UpdateInfo(InformationStudent informationStudent);
        PageResponse<IPagedList<VInfomationStudent>> pagintions(InfomationStudentReq req);
    }
    public class InformationStudentRepo : IInformationStudentRepo
    {
        Project3Context _dbContext;
        public InformationStudentRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }
        public List<InformationStudent> getInformationList()
        {
            return _dbContext.InformationStudents.Where(r => r.Id == 0).ToList();
        }

        public void deleteInformationStudent(InformationStudent informationStudent)
        {
            throw new NotImplementedException();
        }
        public void addOrUpdateInformationStudent(InformationStudent informationStudent)
        {
            if (informationStudent.Id == null)
            {
                _dbContext.InformationStudents.Add(informationStudent);
            }
            else
            {
                _dbContext.InformationStudents.Update(informationStudent);
            }
            _dbContext.SaveChanges();
        }
        // Xóa addOrUpdateInformationStudent
        public void UpdateInfo(InformationStudent informationStudent)
        {
            _dbContext.InformationStudents.Update(informationStudent);
            _dbContext.SaveChanges();

        }


        public InformationStudent getOne(long id)
        {
            return _dbContext.InformationStudents.Where(r => r.Id == id && r.IsDelete == Constants.IsDelete.False).First();
        }

        public void AddInfo(InformationStudent informationStudent)
        {
            _dbContext.InformationStudents.Add(informationStudent);
            _dbContext.SaveChanges();
        }

        public bool exitByEmail(string email)
        {
            return _dbContext.InformationStudents.Any(r => r.Email == email);
        }

        public bool exitIdCardStudent(string idCardStudent)
        {
            return _dbContext.InformationStudents.Any(r => r.IdCardStudent == idCardStudent);
        }

        public PageResponse<IPagedList<VInfomationStudent>> pagintions(InfomationStudentReq req)
        {
            var param = new List<SqlParameter>();
            StringBuilder data = new StringBuilder(" select i.Id,i.FullName,i.DateBirth,i.IdCardStudent,\r\ni.WasBorn,i.IdentityCard,i.StartCard,i.EndCard,\r\ni.FromCard,i.Status,i.Email,i.CreatedAt,i.UpdateAt\r\nfrom InformationStudents as i\r\nwhere i.IsDelete = 0 ");
            if (!string.IsNullOrEmpty(req.fullName))
            {
                data.Append(" and LOWER(b.Title) LIKE '%' + LOWER(@title) + '%' ");
                param.Add(new SqlParameter("@fullName", SqlDbType.NVarChar) { Value = req.fullName.ToLower() });
            }
            if ()
            {
                data.Append("");
                param.Add("");
            }
        }
    }
}
