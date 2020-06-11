using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.Interfaces
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);

        void CreateOrUpdate(ClientBindingModel model);

        void Delete(ClientBindingModel model);

        bool IsAdmin(ClientBindingModel model);

        List<ClientViewModel> ReadAdmins();
    }
}
