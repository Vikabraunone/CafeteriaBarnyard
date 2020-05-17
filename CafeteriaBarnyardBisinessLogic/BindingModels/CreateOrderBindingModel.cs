using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.BindingModels
{
    public class CreateOrderBindingModel
    {
        public int? ClientId { get; set; }

        public decimal Sum { get; set; }

        // Id блюда, название, сумма
        public Dictionary<int, (string, decimal)> OrderDishes { get; set; }
    }
}
