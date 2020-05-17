using CafeteriaBarnyardBisinessLogic.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeteriaBarnyardDatabaseImplement.Models
{
    /// <summary>
    /// Блюдо
    /// </summary>
    public class Dish
    {
        public int Id { get; set; }

        [Required]
        public string DishName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DishType DishType { get; set; }

        [ForeignKey("DishId")]
        public virtual List<OrderDish> OrderDishes { get; set; }

        [ForeignKey("DishId")]
        public virtual List<DishProduct> DishProducts { get; set; }
    }
}
