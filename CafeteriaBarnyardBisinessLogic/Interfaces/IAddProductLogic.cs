using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.Interfaces
{
    public interface IAddProductLogic
    {
        List<AddProductViewModel> Read(AddProductBindingModel model);

        void Create(AddProductBindingModel model);
    }
}
