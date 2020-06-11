using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.Enums;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeteriaBarnyardBisinessLogic.BusinessLogics
{
    public class HelpOrderLogic
    {
        private readonly IOrderLogic orderLogic;
        private readonly IDishLogic dishLogic;

        public HelpOrderLogic(IOrderLogic orderLogic, IDishLogic dishLogic)
        {
            this.orderLogic = orderLogic;
            this.dishLogic = dishLogic;
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            try
            {
                Random rnd = new Random();
                var dishes = dishLogic.Read(null);
                var firstDishes = dishes.Where(x => x.DishType == DishType.Первое).ToList();
                var secondDishes = dishes.Where(x => x.DishType == DishType.Второе).ToList();
                var desserts = dishes.Where(x => x.DishType == DishType.Десерт).ToList();
                var drinks = dishes.Where(x => x.DishType == DishType.Напиток).ToList();
                if (firstDishes.Count() == 0 || secondDishes.Count() == 0 || desserts.Count() == 0 || drinks.Count() == 0)
                    throw new Exception("Отсутствуют 4 разных вида блюд");
                int indexFD = rnd.Next(0, firstDishes.Count());
                int indexSD = rnd.Next(0, secondDishes.Count());
                int indexDessert = rnd.Next(0, desserts.Count());
                int indexDrink = rnd.Next(0, drinks.Count());
                orderLogic.CreateOrUpdate(new OrderBindingModel
                {
                    ClientId = model.ClientId,
                    OrderDishes = new Dictionary<int, (string, decimal)> {
                    { firstDishes[indexFD].Id.Value, (firstDishes[indexFD].DishName, firstDishes[indexFD].Price)},
                    { secondDishes[indexSD].Id.Value, (secondDishes[indexSD].DishName, secondDishes[indexSD].Price)},
                    { desserts[indexDessert].Id.Value, (desserts[indexDessert].DishName, desserts[indexDessert].Price)},
                    { drinks[indexDrink].Id.Value, (drinks[indexDrink].DishName, drinks[indexDrink].Price)},
                },
                    DateCreate = DateTime.Now,
                    OrderSum = firstDishes[indexFD].Price + secondDishes[indexSD].Price + desserts[indexDessert].Price + drinks[indexDrink].Price,
                    Status = OrderStatus.Принят
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
                throw new Exception("Не найден заказ");
            if (order.Status != OrderStatus.Принят)
                throw new Exception("Заказ не в статусе \"Принят\"");
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                OrderDishes = order.OrderDishes,
                OrderSum = order.OrderSum,
                Status = OrderStatus.Выполняется
            });
        }

        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
                throw new Exception("Не найден заказ");
            if (order.Status != OrderStatus.Выполняется)
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                OrderDishes = order.OrderDishes,
                OrderSum = order.OrderSum,
                Status = OrderStatus.Готов
            });
        }

        public void PayOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
                throw new Exception("Не найден заказ");
            if (order.Status != OrderStatus.Готов)
                throw new Exception("Заказ не в статусе \"Готов\"");
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                OrderDishes = order.OrderDishes,
                OrderSum = order.OrderSum,
                Status = OrderStatus.Оплачен
            });
        }
    }
}