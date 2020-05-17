using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using CafeteriaBarnyardDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeteriaBarnyardDatabaseImplement.Implements
{
    public class DishLogic : IDishLogic
    {
        public void CreateOrUpdate(DishBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Dish tempDish = context.Dishes.FirstOrDefault(rec => rec.DishName == model.DishName && rec.Id != model.Id);
                        if (tempDish != null)
                            throw new Exception("Уже есть блюдо с таким названием");
                        if (model.Id.HasValue)
                        {
                            tempDish = context.Dishes.FirstOrDefault(rec => rec.Id == model.Id);
                            if (tempDish == null)
                                throw new Exception("Блюдо не найдено");
                        }
                        else
                        {
                            tempDish = new Dish();
                            context.Dishes.Add(tempDish);
                        }
                        tempDish.DishName = model.DishName;
                        tempDish.Price = model.Price;
                        tempDish.DishType = model.DishType;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var dishProducts = context.DishProducts.Where(rec => rec.DishId == model.Id.Value).ToList();
                            // удалили те, которых нет в модели
                            context.DishProducts.RemoveRange(dishProducts.Where(rec => !model.DishProducts.ContainsKey(rec.ProductId)).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateProduct in dishProducts)
                            {
                                updateProduct.Weight = model.DishProducts[updateProduct.ProductId].Item2;
                                model.DishProducts.Remove(updateProduct.ProductId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var dp in model.DishProducts)
                        {
                            context.DishProducts.Add(new DishProduct
                            {
                                DishId = tempDish.Id,
                                ProductId = dp.Key,
                                Weight = dp.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(DishBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.DishProducts.RemoveRange(context.DishProducts.Where(rec => rec.DishId == model.Id));
                        Dish dish = context.Dishes.FirstOrDefault(rec => rec.Id == model.Id);
                        if (dish != null)
                        {
                            context.Dishes.Remove(dish);
                            context.SaveChanges();
                        }
                        else
                            throw new Exception("Блюдо не найдено");
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<DishViewModel> Read(DishBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                return context.Dishes
                    .Where(rec => model == null || rec.Id == model.Id)
                    .ToList()
                    .Select(rec => new DishViewModel
                    {
                        Id = rec.Id,
                        DishName = rec.DishName,
                        DishType = rec.DishType,
                        Price = rec.Price,
                        DishProducts = context.DishProducts
                        .Include(recDP => recDP.Product)
                        .Where(recDP => recDP.DishId == rec.Id)
                        .ToDictionary(recDP => recDP.ProductId, recDP => (recDP.Product?.ProductName, recDP.Weight))
                    })
                    .ToList();
            }
        }
    }
}
