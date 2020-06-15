using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using CafeteriaBarnyardDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeteriaBarnyardDatabaseImplement.Implements
{
    public class ProductAddingLogic : IProductAddingLogic
    {
        public void Create(ProductAddingBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                Product product = context.Products.FirstOrDefault(rec => rec.Id == model.ProductId);
                if (product == null)
                    throw new Exception("Продукт не найден");
                product.FillWeight += model.Weight;
                context.AddProducts.Add(new ProductAdding
                {
                    ProductId = model.ProductId,
                    DateAdding = model.DateAdding,
                    Weight = model.Weight
                });
                context.SaveChanges();
            }
        }

        public List<ProductAddingViewModel> Read(ProductAddingBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                return context.AddProducts
                .Where(rec => model == null || rec.ProductId == model.ProductId)
                .Select(rec => new ProductAddingViewModel
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
