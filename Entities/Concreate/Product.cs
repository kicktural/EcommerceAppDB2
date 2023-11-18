using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public int Reting { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [NotMapped]
        public string UserId { get; set; }
        public DateTime Createdate { get; set; }
        public User User { get; set; }
        public bool IsFeatured { get; set; }
        public List<ProductLanguage> productLanguages { get; set; }
        public List<Picture> Pictures { get; set; }
    }
}
