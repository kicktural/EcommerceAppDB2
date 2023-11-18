//using Core.DataAccess.EntityFremework;
using Core.DataAccess.EntityFremeworkCore;
using Core.Utilities.Abstract;
using Core.Utilities.Concreate.ErrorResult;
using Core.Utilities.Concreate.SuccessResult;
using DataAccess.Abstract;
using Entities.Concreate;
using Entities.DTOs.CategoryDTOs;
using System.Data.Entity;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace DataAccess.Conceate.EntityFramework
{
    public class EFCategoryDAL : EFRepositoryBASE<Category, AppDBContext>, IcategoryDAL
    {
        public async Task<bool> AddCategoryByLanguages(CategoryAddDTO categoryAddDTO)
        {
            try
            {
                using var context = new AppDBContext();

                Category category = new()
                {
                    PhotoUrl = "//",
                    IsFeatured = false,
                };

                await context.Categoryes.AddAsync(category);
                await context.SaveChangesAsync();

                for (int i = 0; i < categoryAddDTO.CategoryName.Count; i++)
                {
                    CategoryLanguage categoryLanguage = new()
                    {
                        CategoryName = categoryAddDTO.CategoryName[i],
                        CategoryId = category.Id,
                        LangCode = categoryAddDTO.LangCode[i],
                        SeoUrl = "//"
                    };
                    await context.CategoryLanguages.AddAsync(categoryLanguage);
                }
                await context.SaveChangesAsync();



                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IResultData<List<CategoryAdminListDTO>> GetAdminAllCategoriesLanguage(string langCode)
        {
            using AppDBContext context = new();
            try
            {
                var result = context.Categoryes.Select(x => new CategoryAdminListDTO
                {
                    CategoryName = x.CategoryLanguages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
                    PhotoUrl = "//",
                    IsFeatured = x.IsFeatured,
                    Id = x.Id,
                    ProductCount = 0
                }).ToList();

                return new SuccessDataResult<List<CategoryAdminListDTO>>(result);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<CategoryAdminListDTO>>(ex.Message);
            }
        }

        public List<CategoryHomeListDTO> GetAllCategoriesLanguage(string langCode)
        {
            using var context = new AppDBContext();

            return context.CategoryLanguages.
                 Where(x => x.LangCode == langCode)
                 .Include(x => x.Category)
                 .Select(x => new CategoryHomeListDTO
                 {
                     Id = x.Category.Id,
                     CategoryName = x.CategoryName,
                     SeoUrl = x.SeoUrl,
                     PhotoUrl = x.Category.PhotoUrl,
                     ProductCount = 0
                 }).ToList();

        }

        public IEnumerable<CategoryFilterDTO> GetCategoryFilters(string langCode)
        {
           using AppDBContext context = new();

            var result = context.Categoryes.Include(x =>x.CategoryLanguages).Select(x => new CategoryFilterDTO
            {
                Id = x.Id,
                CategoryName = x.CategoryLanguages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
            }).ToList();

            return result;
        }

        public IResultData<List<CategoryFeaturedDTO>> GetFeaturedCategory(string langCode)
        {
            using AppDBContext context = new();

            var result = context.Categoryes.Include(x =>x.CategoryLanguages)
                .Where(x =>x.IsFeatured == true)
                .Select(x => new CategoryFeaturedDTO(x.Id,
                x.CategoryLanguages.FirstOrDefault(x => x.LangCode == langCode).
                CategoryName, x.PhotoUrl,
                x.CategoryLanguages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl, 0)).ToList();

            return new SuccessDataResult<List<CategoryFeaturedDTO>>(result);
        }
    }
}
