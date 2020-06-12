using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeteriaBarnyardDatabaseImplement.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public double FillWeight { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<DishProduct> DishProducts { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<RequestProduct> RequestProducts { get; set; }
    }
}
