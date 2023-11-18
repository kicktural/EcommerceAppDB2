//using Core.DataAccess.EntityFremework;
using Core.DataAccess.EntityFremeworkCore;
using Core.Utilities.Abstract;
using Core.Utilities.Concreate.ErrorResult;
using Core.Utilities.Concreate.SuccessResult;
using Core.Utilities.SeoUrlHelper;
using DataAccess.Abstract;
using Entities.Concreate;
using Entities.DTOs.CardDTOs;
using Entities.DTOs.ProductDTOs;
using Microsoft.EntityFrameworkCore;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace DataAccess.Conceate.EntityFramework
{
    public class EFProductDAL : EFRepositoryBASE<Product, AppDBContext>, IproductDAL
    {
        public IResult AddProduct(string userId, ProductAddDTO productAddDTO)
        {
            try
            {
                using AppDBContext context = new();

                List<Picture> pictureList = new();

                for (int i = 0; i < productAddDTO.PhotoUrls.Count; i++)
                {
                    pictureList.Add(new Picture { PhotoUrl = productAddDTO.PhotoUrls[i] });
                }


                Product product = new()
                {
                    CategoryId = productAddDTO.CategoryId,
                    Price = productAddDTO.Price,
                    Discount = productAddDTO.Discount,
                    Quantity = productAddDTO.Quantity,
                    IsFeatured = productAddDTO.IsFeatured,
                    Pictures = pictureList
                };

                context.Products.Add(product);
                context.SaveChanges();

                for (int i = 0; i < productAddDTO.ProductName.Count; i++)
                {
                    ProductLanguage productLanguage = new()
                    {
                        ProductId = product.Id,
                        ProductName = productAddDTO.ProductName[i],
                        Description = productAddDTO.Description[i],
                        SeoUrl = productAddDTO.ProductName[i].ReplaceInvalidChars(),
                        LangCode = i == 0 ? "az-Az" : i == 1 ? "ru-Ru" : "en-En"
                    };
                    context.ProductLanguages.Add(productLanguage);
                }

                context.SaveChanges();
                return new SuccessResult("Product Added SuccessFully");
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.Message);
            }
        }
        
       
        public IResultData<List<ProductFeaturedDTO>> GetFeaturedProducts(string langCode)
        {          
             using AppDBContext context = new();

            var result = context.Products.Include(x =>x.productLanguages).Include(x =>x.Pictures)
                .Where(x =>x.IsFeatured == true)
                .Select(x => new ProductFeaturedDTO(
                x.Id,
                x.productLanguages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl,
                x.productLanguages.
                FirstOrDefault(x => x.LangCode == langCode).ProductName,
                x.Price,
                x.Discount,
                x.Pictures.FirstOrDefault().PhotoUrl)).ToList();

             return new SuccessDataResult<List<ProductFeaturedDTO>>(result);         

        }

        //public int GetProductCountByCategory(int take, List<int> categoryIds)
        //{
        //    using AppDBContext context = new();
        //    var result = context.Products
        //   .Where(x => categoryIds.Any() == false ? null == null : categoryIds.Contains(x.CategoryId)).Count();
        //    return result;

        //}

        public int GetProductCountByCategory(double take, List<int> categoryIds)
        {
            using AppDBContext context = new();
            var result = context.Products
           .Where(x => categoryIds.Any() == false ? null == null : categoryIds.Contains(x.CategoryId)).Count();
            return result;

        }

        public  ProductDetailDTO GetProductDetail(int id, string langCode)
        {
            using AppDBContext context = new();

            var result = context.Products.Select(x => new ProductDetailDTO
            {
                Id = x.Id,
                ProductName = x.productLanguages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                Description = x.productLanguages.FirstOrDefault(x => x.LangCode == langCode).Description,
                Price = x.Price,
                Discount = x.Discount,
                Quantity = x.Quantity,
                PhotoUrls = x.Pictures.Where(x => x.ProductId == id).Select(x => x.PhotoUrl).ToList(),
            }).FirstOrDefault(x =>x.Id == id);

            //return result.Data;
            return result;
        }

        public IEnumerable<ProductFiltredDTO> GetProductFiltred(List<int> categoryIds, string langCode, int minPrice, int maxPrice, int pageNo, int take)
        {
            using AppDBContext context = new();

            int next = (pageNo - 1) * take;

            var result = context.Products.Include(x => x.productLanguages).Include(x => x.Pictures).Where(x => x.Price >= minPrice && x.Price <= maxPrice && (categoryIds.Any()? categoryIds.Contains(x.CategoryId) : null == null))
                .Select(x => new ProductFiltredDTO
                {
                    Id = x.Id,
                    Name = x.productLanguages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                    Price = x.Price,
                    Discount = x.Discount,
                    PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl,
                }).Skip(next).Take(take).ToList();

            return result;
        }

        public IResultData<List<ProductRecentDTO>> GetRecentProducts(string langCode)
        {
            using AppDBContext context = new();

            var result = context.Set<ProductRecentDTO>().FromSqlInterpolated($"exec GetRecentProducts @LangCode = {langCode}").ToList();
            return new SuccessDataResult<List<ProductRecentDTO>>(result);
        }

        public List<UserCartDTO> GetUserCart(List<int> ids, string langCode)
        {
            using AppDBContext context = new();
            var result = context.Products.Where(x => ids.Contains(x.Id))
                .Include(x =>x.productLanguages)
                .Include(x =>x.Pictures)
                .Select(x => new UserCartDTO
            {
                Id = x.Id,
                ProductName = x.productLanguages.FirstOrDefault(x =>x.LangCode == langCode).ProductName,
                Price= x.Price,          
                PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl
            }).ToList();

            return result;
        }

        IResultData<List<ProductAdminListDTO>> IproductDAL.GetProductBYUser(string userId, string? langCode)
        {
            try
            {

                using AppDBContext context = new();

                var products = context.Products
                    .Include(x => x.Category.CategoryLanguages)
                    .Include(x => x.productLanguages)
                    .Include(x => x.Pictures)
                    .Select(x => new ProductAdminListDTO
                    {
                        ProductName = x.productLanguages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                        Id = x.Id,
                        Price = x.Price,
                        Discount = x.Discount,
                        CategoryName = x.Category.CategoryLanguages.FirstOrDefault(x => x.LangCode == "Az").CategoryName,
                        PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl,

                    }).ToList();

                return new SuccessDataResult<List<ProductAdminListDTO>>(products, "Products were delivared successFully");
            }
            catch (Exception exception)
            {

                return new ErrorDataResult<List<ProductAdminListDTO>>(exception.Message);
                
            }
        }

   
    }
}
