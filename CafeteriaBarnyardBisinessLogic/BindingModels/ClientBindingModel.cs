﻿namespace CafeteriaBarnyardBisinessLogic.BindingModels
{
    public class ClientBindingModel
    {
        public int? Id { get; set; }

        public string ClientFIO { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
