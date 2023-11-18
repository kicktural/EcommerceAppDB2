using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class Order
    {
        public int Id { get; set; }
        [NotMapped]
        public string UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantiry { get; set; }
        public string Message { get; set; }
    }
}
