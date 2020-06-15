using System;
using System.ComponentModel;

namespace CafeteriaBarnyardBisinessLogic.ViewModels
{
    // заявки и заказы с расшифровкой по продуктам
    public class ReportRequestAndOrderProductsViewModel
    {
        [DisplayName("Заявка")]
        public int? Id { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Блюдо")]
        public string DishName { get; set; }

        [DisplayName("Продукт")]
        public string ProductName { get; set; }

        [DisplayName("Вес/количество")]
        public double Count { get; set; }
    }
}
