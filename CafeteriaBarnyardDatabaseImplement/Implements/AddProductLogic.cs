using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using CafeteriaBarnyardDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeteriaBarnyardDatabaseImplement.Implements
{
    public class AddProductLogic : IAddProductLogic
    {
        public void Create(AddProductBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                Product product = context.Products.FirstOrDefault(rec => rec.Id == model.ProductId);
                if (product == null)
                    throw new Exception("Продукт не найден");
                product.FillWeight += model.Weight;
                context.AddProducts.Add(new AddProduct
                {
                    ProductId = model.ProductId,
                    DateAdding = model.DateAdding,
                    Weight = model.Weight
                });
                context.SaveChanges();
            }
        }

        public List<AddProductViewModel> Read(AddProductBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                return context.AddProducts
                .Where(rec => model == null || rec.ProductId == model.ProductId)
                .Select(rec => new AddProductViewModel
                {
                    Id = rec.Id,
                    ProductId = rec.ProductId,
                    DateAdding = rec.DateAdding,
                    ProductName = context.Products.FirstOrDefault(recP => recP.Id == model.ProductId).ProductName,
                    Weight = rec.Weight
                })
                .ToList();
            }
        }
    }
}
