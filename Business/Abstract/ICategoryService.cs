using Core.Utilities.Abstract;
using Core.Utilities.Concreate;
using Entities.Concreate;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IResult AddCategory(CategoryAddDTO category);
        IResult DeleteCategory(Category category);
        IResult UpdateCategory(Category category);
        IResultData<List<CategoryHomeListDTO>> GetAllCategories(string langCode);
        List<Category> GetAllNavbarCategories();         
        IResultData<List<CategoryAdminListDTO>> GetAllAdminCategories(string langCode);
        IResultData<List<CategoryFeaturedDTO>> GetAllFeaturedCategory(string langCode);
        IResultData<IEnumerable<CategoryFilterDTO>> GetAllFilterCategories(string langCode);
        IResultData<int> GetAllCategories();
    }
}
