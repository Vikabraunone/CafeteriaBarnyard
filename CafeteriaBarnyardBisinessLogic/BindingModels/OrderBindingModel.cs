using CafeteriaBarnyardBisinessLogic.Enums;
using System;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.BindingModels
{
    public class OrderBindingModel
    {
        public int? Id { get; set; }

        public int? ClientId { get; set; }

        public decimal FillSum { get; set; }

        public DateTime DateCreate { get; set; }

        public OrderStatus Status { get; set; }

        // Id блюда - сумма
        public Dictionary<int, (int, double)> OrderDishes { get; set; }
    }
}
