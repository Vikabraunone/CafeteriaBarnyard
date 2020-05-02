using System;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.BindingModels
{
    public class RequestProductBindingModel
    {
        public int? Id { get; set; }

        public DateTime DateRequest { get; set; }

        // название продукта - вес
        public Dictionary<int, (string, double)> RequestProducts { get; set; }
    }
}
