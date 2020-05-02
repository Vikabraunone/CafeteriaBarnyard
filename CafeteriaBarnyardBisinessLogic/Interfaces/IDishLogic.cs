using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.Interfaces
{
    public interface IDishLogic
    {
        List<DishViewModel> Read(DishBindingModel model);

        void CreateOrUpdate(DishBindingModel model);

        void Delete(DishBindingModel model);
    }
}
