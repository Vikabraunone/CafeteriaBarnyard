using System.ComponentModel.DataAnnotations;

namespace CafeteriaBarnyardDatabaseImplement.Models
{
    public class RequestProduct
    {
        public int Id { get; set; }

        public int RequestId { get; set; }

        public int ProductId { get; set; }

        [Required]
        public double Weight { get; set; }

        public virtual Request Request { get; set; }

        public virtual Product Product { get; set; }
    }
}
