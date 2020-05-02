using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CafeteriaBarnyardBisinessLogic.ViewModels
{
    public class RequestProductViewModel
    {
        [DisplayName("Номер заявки")]
        public int? Id { get; set; }

        [DisplayName("Дата заявки")]
        public DateTime DateRequest { get; set; }

        // название продукта - вес
        public Dictionary<int, (string, double)> RequestProducts { get; set; }
    }
}
