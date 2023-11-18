using AutoMapper;
using Business.Abstract;
using Business.AutoMapper;
using Business.Concreate;
using DataAccess.Abstract;
using DataAccess.Conceate.EntityFramework;
//using DataAccess.Conceate.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolver
{
    public static class ServiceRegistiration
    {

        public static void Create(this IServiceCollection services)
        {
            

            services.AddScoped<AppDBContext>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IcategoryDAL, EFCategoryDAL>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IproductDAL, EFProductDAL>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOrderDAL, EFOrderDAL>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        } 
    }
}
