using Entities.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace DataAccess.Conceate.EntityFramework
{
    public class AppDBContext : IdentityDbContext<User>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = LOCALHOST\\SQLEXPRESS; DataBase =" +
                " EcommerceAppDB1;" +
                " Trusted_Connection = True;" +
                " MultipleActiveResultSets = True;" +
                " TrustServerCertificate = True;");
        }
        public DbSet<Category> Categoryes { get; set; }
        public DbSet<CategoryLanguage> CategoryLanguages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLanguage> ProductLanguages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<ProductRecentDTO>().ToView(null);
        }
    }
}
