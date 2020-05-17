using CafeteriaBarnyardBisinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CafeteriaBarnyardBisinessLogic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Блюдо")]
        public Dictionary<int, (string, decimal)> OrderDishes { get; set; }

        [DisplayName("Сумма")]
        public decimal OrderSum { get; set; }

        public OrderStatus Status { get; set; }
    }
}
