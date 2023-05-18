using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Migrations;
using Project3.Models;
using Project3.Models.View;
using Project3.Utils;
using System.Data;
using System.Reflection.Metadata;
using System.Text;

namespace Project3.Repositories
{
    public interface ICategoryBlogRepo
    {
        List<CategoryBlog> getCategoryList();
        void addOrUpdateCategoryBlog(CategoryBlog categoryblog);
        void deleteCategoryBlog(CategoryBlog categoryBlog);
        CategoryBlog getOne(long id);
        bool exitCategoryBlogId(long id);
        PageResponse<IPagedList<VCategoryBlogs>> search(CategoryBlogReq filter);
    }
    public class CategoryBlogRepo : ICategoryBlogRepo
    {
        Project3Context _dbContext;
        public CategoryBlogRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void addOrUpdateCategoryBlog(CategoryBlog categoryBlog)
        {
            if ( categoryBlog.Id == null)
            {
                _dbContext.CategoryBlogs.Add(categoryBlog);
            }
            else
            {
                _dbContext.CategoryBlogs.Update(categoryBlog);
            }
            _dbContext.SaveChanges();
        }

        public void deleteCategoryBlog(CategoryBlog categoryBlog)
        {
            _dbContext.CategoryBlogs.Update(categoryBlog);
            _dbContext.SaveChanges();
        }

        public List<CategoryBlog> getCategoryList()
        {
            return _dbContext.CategoryBlogs.Where(r => r.Id == 0).ToList();
        }

        public CategoryBlog getOne(long id)
        {
            var data = _dbContext.CategoryBlogs.Where(r => r.Id == id).First();

            return data;
        }

        public bool exitCategoryBlogId(long id)
        {
            return _dbContext.CategoryBlogs.Any(r => r.Id == id && r.IsDelete == Constants.IsDelete.False); ;
        }

        public PageResponse<IPagedList<VCategoryBlogs>> search(CategoryBlogReq filter)
        {
            var param = new List<SqlParameter>();
            StringBuilder data = new StringBuilder(" select * from V_CategoryBlogs as b where 1 = 1 ");

            if (!string.IsNullOrEmpty(filter.Name))
            {
                data.Append(" and LOWER(b.CategoryName) LIKE '%' + @name + '%' ");
                param.Add(new SqlParameter("@name", SqlDbType.NVarChar) { Value = filter.Name.ToLower() });
            }
            if (filter.Id != null)
            {
                data.Append(" and b.Id = @id");
                param.Add(new SqlParameter("@id", SqlDbType.VarChar) { Value = filter.Id });
            }
            var query = _dbContext.Set<VCategoryBlogs>().FromSqlRaw(data.ToString(), param.ToArray())
                .OrderBy(r => r.CategoryName).ThenByDescending(r => r.CreatedAt);

            var total = query.Count();

            var pageData = query.ToPagedList((int)filter.pageNumber, (int)filter.pageSize);

            var pageTotal = Math.Round((decimal)total / (int)filter.pageSize);

            return new PageResponse<IPagedList<VCategoryBlogs>>(pageData, (int)filter.pageNumber, (int)filter.pageSize, total, (int)pageTotal);
        }
    }
}
