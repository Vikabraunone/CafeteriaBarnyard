using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeteriaBarnyardDatabaseImplement.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string ClientFIO { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        [ForeignKey("ClientId")]
        public virtual List<Order> Orders { get; set; }
    }
}
