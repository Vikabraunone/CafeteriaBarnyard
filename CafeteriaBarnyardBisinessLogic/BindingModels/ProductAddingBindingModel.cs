using System;

namespace CafeteriaBarnyardBisinessLogic.BindingModels
{
    /// <summary>
    /// Пополнение продуктов
    /// </summary>
    public class ProductAddingBindingModel
    {
        public int? Id { get; set; }

        public int ProductId { get; set; }

        public int Weight { get; set; }

        public DateTime DateAdding { get; set; }
    }
}
