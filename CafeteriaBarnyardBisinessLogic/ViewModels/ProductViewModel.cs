using System.ComponentModel;

namespace CafeteriaBarnyardBisinessLogic.ViewModels
{
    public class ProductViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Название продукта")]
        public string ProductName { get; set; }

        [DisplayName("В наличии")]
        public double? FillWeight { get; set; }

        [DisplayName("Цена продукта")]
        public decimal Price { get; set; }
    }
}
