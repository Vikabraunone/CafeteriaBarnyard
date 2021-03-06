﻿using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using CafeteriaBarnyardDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeteriaBarnyardDatabaseImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Order order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                        if (order == null)
                        {
                            order = new Order
                            {
                                ClientId = model.ClientId.Value,
                                OrderSum = model.OrderSum,
                                DateCreate = model.DateCreate,
                                Status = model.Status
                            };
                            context.Orders.Add(order);
                            context.SaveChanges();
                            foreach (var od in model.OrderDishes)
                            {
                                context.OrderDishes.Add(new OrderDish
                                {
                                    OrderId = order.Id,
                                    DishId = od.Key,
                                    DishPrice = od.Value.Item2
                                });
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            order.Status = model.Status;
                            order.DateImplement = model.DateImplement;
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

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                return context.Orders
                    .Where(rec => model == null || rec.Id == model.Id ||
                     rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
                    .ToList()
                    .Select(rec => new OrderViewModel
                    {
                        Id = rec.Id,
                        ClientId = rec.ClientId,
                        ClientFIO = context.Orders
                        .Include(recC => recC.Client)
                        .Where(recC => recC.ClientId == rec.ClientId)
                        .ToList()[0].Client.ClientFIO,
                        DateCreate = rec.DateCreate,
                        Status = rec.Status,
                        OrderSum = rec.OrderSum,
                        OrderDishes = context.OrderDishes
                        .Include(recOD => recOD.Dish)
                        .Where(recOD => recOD.OrderId == rec.Id)
                        .ToDictionary(recOD => recOD.DishId, recOD => (recOD.Dish?.DishName, recOD.DishPrice))
                    })
                    .ToList();
            }
        }
    }
}
