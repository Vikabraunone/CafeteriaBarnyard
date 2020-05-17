using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.Interfaces
{
    public interface IRequest
    {
        List<RequestViewModel> Read(RequestBindingModel model);

        void Create(RequestBindingModel model);
    }
}
