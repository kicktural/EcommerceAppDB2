using Core.Utilities.Abstract;
using Entities.DTOs.CardDTOs;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace Business.Abstract
{
    public interface IProductService
    {
        IResultData<List<ProductAdminListDTO>> GetDashboardProductS(string userId, string langCode);
        IResult AddProductAdmin(string userId, ProductAddDTO productAdd);
        IResultData<List<ProductFeaturedDTO>> GetAllFeaturedProduts(string langCode);
        IResultData<List<ProductRecentDTO>> GetAllRecentProduts(string langCode);
        IResultData<ProductDetailDTO> GetProductBYId(int id, string langCode);
        IResultData<IEnumerable<ProductFiltredDTO>> GetAllFiltredProducts(List<int> categoryIds, string langCode, int minPrice, int maxPrice, int pageNo, int take);
        IResultData<int> GetProductCount(double take, List<int> categoryIds);
        IResultData<int> GetProductQuantityById(int productId);
        IResultData<List<UserCartDTO>> GetProductForCart(List<int> ids, string langCode, List<int> quantity );
    }
}
