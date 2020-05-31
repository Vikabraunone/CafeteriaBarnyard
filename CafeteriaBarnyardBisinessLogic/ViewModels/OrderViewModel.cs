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

        [DisplayName("Клиент")]
        public string ClientFIO { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Блюдо")]
        public Dictionary<int, (string, decimal)> OrderDishes { get; set; }

        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }

        [DisplayName("Сумма заказа")]
        public decimal OrderSum { get; set; }
    }
}
