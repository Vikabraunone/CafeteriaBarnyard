using System;

namespace CafeteriaBarnyardDatabaseImplement.Models
{
    public class AddProduct
    {
        public int? Id { get; set; }

        public int ProductId { get; set; }

        public int Weight { get; set; }

        public DateTime DateAdding { get; set; }
    }
}
