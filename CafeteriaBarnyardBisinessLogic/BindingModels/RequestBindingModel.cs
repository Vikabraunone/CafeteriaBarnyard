using System;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.BindingModels
{
    /// <summary>
    /// Заявки на продукты
    /// </summary>
    public class RequestBindingModel
    {
        public int? Id { get; set; }

        public int? ClientId { get; set; }

        public DateTime DateRequest { get; set; }

        // Продукт, название, вес
        public Dictionary<int, (string, double)> RequestProducts { get; set; }
    }
}
