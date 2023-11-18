using Business.Abstract;
using Core.Utilities.Abstract;
using Core.Utilities.Concreate.ErrorResult;
using Core.Utilities.Concreate.SuccessResult;
using DataAccess.Abstract;
using Entities.DTOs.CardDTOs;
using Entities.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace Business.Concreate
{
     public class ProductManager : IProductService
    {
        private readonly IproductDAL _productDAL;
       
        public ProductManager(IproductDAL productDAL)
        {
            _productDAL = productDAL;         
        }

        public IResult AddProductAdmin(string userId, ProductAddDTO productAdd)
        {
            var result = _productDAL.AddProduct(userId, productAdd);
            if (result.Success)
            {
                return new SuccessResult(result.Message);
            }
            return new ErrorResult(result.Message);
        }

        public IResultData<List<ProductFeaturedDTO>> GetAllFeaturedProduts(string langCode)
        {
            var result = _productDAL.GetFeaturedProducts(langCode);
            return new SuccessDataResult<List<ProductFeaturedDTO>>(result.Data);
        }

        public IResultData<IEnumerable<ProductFiltredDTO>> GetAllFiltredProducts(List<int> categoryIds, string langCode, int minPrice, int maxPrice, int pageNo, int take)
        {
            var result = _productDAL.GetProductFiltred(categoryIds, langCode, minPrice, maxPrice, pageNo, take);
            return new SuccessDataResult<IEnumerable<ProductFiltredDTO>>(result);
        }

       

        public IResultData<List<ProductRecentDTO>> GetAllRecentProduts(string langCode)
        {
            var result = _productDAL.GetRecentProducts(langCode);

            return new SuccessDataResult<List<ProductRecentDTO>>(result.Data);
        }

        public IResultData<List<ProductAdminListDTO>> GetDashboardProductS(string userId, string langCode)
        {
            
            var result = _productDAL.GetProductBYUser(userId, langCode);
            if (result.Success)
            {
                return new SuccessDataResult<List<ProductAdminListDTO>>(result.Data);
            }
            return new ErrorDataResult<List<ProductAdminListDTO>>(result.Message);
        }

        public IResultData<ProductDetailDTO> GetProductBYId(int id, string langCode)
        {
            var result = _productDAL.GetProductDetail(id, langCode);

            return new SuccessDataResult<ProductDetailDTO>(result);
        }



        public IResultData<int> GetProductCount(double take, List<int> categoryIds)
        {
            double res = _productDAL.GetProductCountByCategory(take, categoryIds) / take;
            int productCountResult = (int)Math.Ceiling((double)res);

            return new SuccessDataResult<int>(productCountResult);

        }

        public IResultData<List<UserCartDTO>> GetProductForCart(List<int> ids, string langCode, List<int> quantity)
        {
            var result = _productDAL.GetUserCart(ids, langCode);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Quantity = quantity[i];
            }
            return new SuccessDataResult<List<UserCartDTO>>(result);
        }

        public IResultData<int> GetProductQuantityById(int productId)
        {
            var productCount = _productDAL.Get(x => x.Id == productId).Quantity;
            return new SuccessDataResult<int>(productCount);
        }
    }
}
