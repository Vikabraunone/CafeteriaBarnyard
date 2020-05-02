using CafeteriaBarnyardBisinessLogic.Enums;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.BindingModels
{
    public class DishBindingModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DishType DishType { get; set; }

        // Название продукта и его количество
        public Dictionary<int, (string, double)> DishProducts { get; set; }
    }
}
