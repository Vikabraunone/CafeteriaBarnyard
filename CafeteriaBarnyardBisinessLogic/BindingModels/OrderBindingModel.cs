using CafeteriaBarnyardBisinessLogic.Enums;
using System;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.BindingModels
{
    public class OrderBindingModel
    {
        public int? Id { get; set; }

        public int? ClientId { get; set; }

        // Сумма заказа
        public decimal OrderSum { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public OrderStatus Status { get; set; }

        // Id блюда, название, сумма
        public Dictionary<int, (string, decimal)> OrderDishes { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
