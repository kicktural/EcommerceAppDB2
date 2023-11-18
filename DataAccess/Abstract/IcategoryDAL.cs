using Core.DataAccess;
using Core.Utilities.Abstract;
using Entities.Concreate;
using Entities.DTOs.CategoryDTOs;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace DataAccess.Abstract
{
    public interface IcategoryDAL : IReposiToryBase<Category>
    {
       List<CategoryHomeListDTO> GetAllCategoriesLanguage(string langCode);
       Task<bool> AddCategoryByLanguages(CategoryAddDTO categoryAddDTO);
       IResultData<List<CategoryAdminListDTO>> GetAdminAllCategoriesLanguage(string langCode);
       IResultData<List<CategoryFeaturedDTO>> GetFeaturedCategory(string langCode);
       IEnumerable<CategoryFilterDTO> GetCategoryFilters(string langCode);
    }
}
