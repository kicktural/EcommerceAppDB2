using Business.Abstract;
using Core.Utilities.Abstract;
using Core.Utilities.Concreate;
using Core.Utilities.Concreate.ErrorResult;
using Core.Utilities.Concreate.SuccessResult;
using DataAccess.Abstract;
using Entities.Concreate;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace Business.Concreate
{
    public class CategoryManager : ICategoryService
    {
        private readonly IcategoryDAL _categoryDAL;

        public CategoryManager(IcategoryDAL ıcategoryDAL)
        {
            _categoryDAL = ıcategoryDAL;
        }

        public IResult AddCategory(CategoryAddDTO category)
        {
            try
            {
                _categoryDAL.AddCategoryByLanguages(category);
                return new SuccessResult("Product Added SuccessFully!");
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.Message);
            }
        }

        public IResult DeleteCategory(Category category)
        {
            throw new NotImplementedException();
            
        }

        public IResultData<List<CategoryAdminListDTO>> GetAllAdminCategories(string langCode)
        {
            var result = _categoryDAL.GetAdminAllCategoriesLanguage(langCode);
            if (result.Success)
            {
              return new SuccessDataResult<List<CategoryAdminListDTO>>(result.Data);
            }
            return new ErrorDataResult<List<CategoryAdminListDTO>>(result.Message);
        }
        

        public IResultData<List<CategoryHomeListDTO>> GetAllCategories(string langCode)
        {
            var result = _categoryDAL.GetAllCategoriesLanguage(langCode);
             return new SuccessDataResult<List<CategoryHomeListDTO>>(result, "All Categories");
        }

        public IResultData<int> GetAllCategories()
        {
            var result = _categoryDAL.GetAll().Count;
            return new SuccessDataResult<int>(result);
        }

        public IResultData<List<CategoryFeaturedDTO>> GetAllFeaturedCategory(string langCode)
        {
            var result = _categoryDAL.GetFeaturedCategory(langCode);
            return new SuccessDataResult<List<CategoryFeaturedDTO>>(result.Data);
        }

        public IResultData<IEnumerable<CategoryFilterDTO>> GetAllFilterCategories(string langCode)
        {
           var result = _categoryDAL.GetCategoryFilters(langCode);
            return new SuccessDataResult<IEnumerable<CategoryFilterDTO>>(result);
        }

        public List<Category> GetAllNavbarCategories()
        {
            throw new NotImplementedException();
        }

        public IResult UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
