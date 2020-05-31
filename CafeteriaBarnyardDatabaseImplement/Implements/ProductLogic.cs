using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using CafeteriaBarnyardDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeteriaBarnyardDatabaseImplement.Implements
{
    public class ProductLogic : IProductLogic
    {
        public void CreateOrUpdate(ProductBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                Product element = context.Products.FirstOrDefault(rec => rec.ProductName == model.ProductName && rec.Id != model.Id);
                if (element != null)
                    throw new Exception("Уже есть продукт с таким названием");
                if (model.Id.HasValue)
                {
                    element = context.Products.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                        throw new Exception("Продукт не найден");
                }
                else
                {
                    element = new Product();
                    context.Products.Add(element);
                }
                element.ProductName = model.ProductName;
                element.Price = model.Price;
                if (model.FillWeight.HasValue)
                    element.FillWeight = model.FillWeight.Value;
                context.SaveChanges();
            }
        }

        public void Delete(ProductBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                Product element = context.Products.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Products.Remove(element);
                    context.SaveChanges();
                }
                else
                    throw new Exception("Продукт не найден");
            }
        }

        public List<ProductViewModel> Read(ProductBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                var list = context.Products
                 .Where(rec => model == null || rec.Id == model.Id || rec.ProductName.Equals(model.ProductName))
                 .Select(rec => new ProductViewModel
                 {
                     Id = rec.Id,
                     ProductName = rec.ProductName,
                     FillWeight = rec.FillWeight,
                     Price = rec.Price
                 })
                 .ToList();
                return list;
            }
        }
    }
}
