using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.BindingModels
{
    public class CreateOrderBindingModel
    {
        public int? ClientId { get; set; }

        public decimal Sum { get; set; }

        public Dictionary<int, (int, double)> OrderDishes { get; set; }
    }
}
