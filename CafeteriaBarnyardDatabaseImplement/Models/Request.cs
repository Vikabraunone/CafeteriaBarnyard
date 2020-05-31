using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeteriaBarnyardDatabaseImplement.Models
{
    /// <summary>
    /// Заявки на продукты
    /// </summary>
    public class Request
    {
        public int? Id { get; set; }

        public int? ClientId { get; set; }

        public DateTime DateRequest { get; set; }

        [ForeignKey("RequestId")]
        public virtual List<RequestProduct> RequestProducts { get; set; }
    }
}
