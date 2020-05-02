using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.Interfaces
{
    public interface IRequestProduct
    {
        List<RequestProductViewModel> Read(RequestProductBindingModel model);

        void CreateOrUpdate(RequestProductBindingModel model);

        void Delete(RequestProductBindingModel model);
    }
}
