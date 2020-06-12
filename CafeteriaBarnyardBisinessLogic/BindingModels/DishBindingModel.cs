using CafeteriaBarnyardBisinessLogic.Enums;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.BindingModels
{
    /// <summary>
    /// Блюдо
    /// </summary>
    public class DishBindingModel
    {
        public int? Id { get; set; }

        public string DishName { get; set; }

        public decimal Price { get; set; }

        public DishType DishType { get; set; }

        // Id продукта, название и его количество
        public Dictionary<int, (string, double)> DishProducts { get; set; }
    }
}
