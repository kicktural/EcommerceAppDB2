using Core.DataAccess;
using Core.Utilities.Abstract;
using Entities.Concreate;
using Entities.DTOs.CardDTOs;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace DataAccess.Abstract
{
    public interface IproductDAL : IReposiToryBase<Product>
    {
        IResultData<List<ProductAdminListDTO>> GetProductBYUser(string userId, string? langCode = "Az");
        IResult AddProduct(string userId, ProductAddDTO productAddDTO);
        IResultData<List<ProductFeaturedDTO>> GetFeaturedProducts(string langCode);
        IResultData<List<ProductRecentDTO>> GetRecentProducts(string langCode);
        ProductDetailDTO GetProductDetail(int id, string langCode);
        IEnumerable<ProductFiltredDTO> GetProductFiltred(List<int> categoryIds, string langCode, int minPrice, int maxPrice, int pageNo, int take);
        //int GetProductCountByCategory(int take, List<int> categoryIds);
        int GetProductCountByCategory(double take, List<int> categoryIds);
        List<UserCartDTO> GetUserCart(List<int> ids, string langCode);
    }
}
