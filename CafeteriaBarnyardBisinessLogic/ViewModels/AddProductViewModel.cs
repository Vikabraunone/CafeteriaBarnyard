using System;
using System.ComponentModel;

namespace CafeteriaBarnyardBisinessLogic.ViewModels
{
    public class AddProductViewModel
    {
        public int? Id { get; set; }

        public int ProductId { get; set; }

        [DisplayName("Продукт")]
        public string ProductName { get; set; }

        [DisplayName("Приход")]
        public int Weight { get; set; }

        [DisplayName("Дата прихода")]
        public DateTime DateAdding { get; set; }
    }
}
