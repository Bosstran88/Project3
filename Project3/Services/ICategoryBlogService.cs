using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;
using Project3.Utils;

namespace Project3.Services
{
    public interface ICategoryBlogService
    {
        BaseResponse getOne(long id);
        BaseResponse deleteCategoy(long id);
        BaseResponse createOrUpdate(AddCategoryBlog categoryBlog);
    }
    public class CategoryBlogService : ICategoryBlogService
    {
        ICategoryBlogRepo _categoryBlogRepo;
        CategoryBlog categoryBlog;

        public CategoryBlogService(ICategoryBlogRepo categoryBlogRepo)
        {
            _categoryBlogRepo = categoryBlogRepo;
        }

        public BaseResponse createOrUpdate(AddCategoryBlog categoryBlog)
        {
            if (categoryBlog.Id == null)
            {
                this.categoryBlog = new CategoryBlog();
                this.categoryBlog.CreatedAt = DateTime.Now;
            }
            else
            {
                this.categoryBlog = _categoryBlogRepo.getOne((long)categoryBlog.Id);
                if(this.categoryBlog == null)
                {
                    throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
                }
                this.categoryBlog.UpdateAt = DateTime.Now;
            }

            convertFromDtoToModel(categoryBlog);
            _categoryBlogRepo.addOrUpdateCategoryBlog(this.categoryBlog);
            return new BaseResponse();
        }
        private void convertFromDtoToModel(AddCategoryBlog categories)
        {
            categoryBlog.Id = (long)categories.Id;
            categoryBlog.CategoryName = categories.CategoryName;
            categoryBlog.Description = categories.Description;

        }

        public BaseResponse deleteCategoy(long id)
        {
            var data = _categoryBlogRepo.getOne(id);
            if(data == null)
            {
                throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            }
            data.UpdateAt = DateTime.Now;
            _categoryBlogRepo.deleteCategoryBlog(data);
            return new BaseResponse();
        }

        public BaseResponse getOne(long id)
        {
            var data = _categoryBlogRepo.getOne(id);
            if(data == null)
            {
                return new BaseResponse();
            }
            var format = new VCategoryOne
            {
                Id = data.Id,
                CategoryName = data.CategoryName,
                Description = data.Description,
                CreatedId = data.CreatedId,
                CreatedAt = data.CreatedAt
            };
            return new BaseResponse(format);
        }

      
    }
}
