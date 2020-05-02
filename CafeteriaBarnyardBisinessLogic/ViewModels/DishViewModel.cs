using CafeteriaBarnyardBisinessLogic.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace CafeteriaBarnyardBisinessLogic.ViewModels
{
    public class DishViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Название блюда")]
        public string DishName { get; set; }

        [DisplayName("Цена блюда")]
        public decimal FillPrice { get; set; }

        [DisplayName("Тип блюда")]
        public DishType DishType { get; set; }

        // Название продукта и его количество
        public Dictionary<int, (string, double)> DishProducts { get; set; }
    }
}
