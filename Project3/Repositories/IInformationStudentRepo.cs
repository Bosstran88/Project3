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
using Microsoft.EntityFrameworkCore;

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
                data.Append(" and LOWER(i.FullName) LIKE '%' + @fullName + '%' ");
                param.Add(new SqlParameter("@fullName", SqlDbType.NVarChar) { Value = req.fullName.ToLower() });
            }
            if (!string.IsNullOrEmpty(req.idCardStudent))
            {
                data.Append(" and i.IdCardStudent = @card ");
                param.Add(new SqlParameter("@card", SqlDbType.VarChar ) { Value = req.idCardStudent });
            }
            if (!string.IsNullOrEmpty(req.status))
            {
                data.Append(" and i.Status = @status ");
                param.Add(new SqlParameter("@status", SqlDbType.NVarChar ) { Value = req.status });
            }
            if(req.created_From != null && req.Created_To != null) 
            {
                data.Append(" and i.CreatedAt >= @createFrom and i.CreatedAt <= @createTo ");
                param.Add(new SqlParameter("@createFrom", SqlDbType.DateTime) { Value = req.created_From });
                param.Add(new SqlParameter("@createTo", SqlDbType.DateTime) { Value = req.Created_To });
            }
            var query = _dbContext.Set<InformationStudent>().FromSqlRaw(data.ToString(),param.ToArray()).OrderBy(r => r.FullName).ThenByDescending(r => r.CreatedAt)
                .Select(r => new VInfomationStudent
                {
                    id = r.Id,
                    fullName= r.FullName,
                    DateBirth = r.DateBirth,
                    idCardStudent = r.IdCardStudent,
                    wasBorn = r.WasBorn,
                    identityCard = r.IdentityCard,
                    startCard = r.StartCard,
                    endCard = r.EndCard,
                    fromCard = r.FromCard,
                    status = r.Status,
                    email = r.Email,
                    createAt = r.CreatedAt,
                    updatedAt = r.UpdateAt
                });
            var total = query.Count();

            var pageData = query.ToPagedList((int)req.pageNumber, (int)req.pageSize);

            var pageTotal = Math.Round((decimal)total / (int)req.pageSize);

            return new PageResponse<IPagedList<VInfomationStudent>>(pageData, (int)req.pageNumber, (int)req.pageSize, total, (int)pageTotal);

        }
    }
}
