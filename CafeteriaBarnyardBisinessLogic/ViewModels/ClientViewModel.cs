using System.ComponentModel;

namespace CafeteriaBarnyardBisinessLogic.ViewModels
{
    public class ClientViewModel
    {
        public int? Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        [DisplayName("Клиент")]
        public string ClientFIO { get; set; }
    }
}
