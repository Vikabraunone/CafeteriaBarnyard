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
    public class RequestLogic : IRequestLogic
    {
        public bool Create(RequestBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Request request = new Request
                        {
                            ClientId = model.ClientId,
                            DateRequest = model.DateRequest
                        };
                        context.Requests.Add(request);
                        context.SaveChanges();
                        Product product;
                        foreach (var rp in model.RequestProducts)
                        {
                            product = context.Products.First(rec => rec.Id == rp.Key);
                            if (product.FillWeight <= rp.Value.Item2)
                            {
                                transaction.Rollback();
                                return false;
                            }
                            product.FillWeight -= rp.Value.Item2;
                            context.RequestProducts.Add(new RequestProduct
                            {
                                RequestId = request.Id.Value,
                                ProductId = rp.Key,
                                Weight = rp.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

            }
        }

        public List<RequestViewModel> Read(RequestBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                return context.Requests
                    .Where(rec => model == null || rec.Id == model.Id)
                    .ToList()
                    .Select(rec => new RequestViewModel
                    {
                        Id = rec.Id,
                        DateRequest = rec.DateRequest,
                        RequestProducts = context.RequestProducts
                        .Include(recRP => recRP.ProductId)
                        .Where(recPR => recPR.RequestId == rec.Id)
                        .ToDictionary(recPR => recPR.ProductId, recPR =>
                        (recPR.Product?.ProductName, recPR.Weight))
                    })
                    .ToList();
            }
        }
    }
}
