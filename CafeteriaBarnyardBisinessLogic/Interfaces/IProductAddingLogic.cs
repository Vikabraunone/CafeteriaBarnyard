using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.Interfaces
{
    public interface IProductAddingLogic
    {
        List<ProductAddingViewModel> Read(ProductAddingBindingModel model);

        void Create(ProductAddingBindingModel model);
    }
}
