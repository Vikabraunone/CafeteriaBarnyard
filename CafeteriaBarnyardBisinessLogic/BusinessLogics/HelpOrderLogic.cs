using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.Enums;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using System;

namespace CafeteriaBarnyardBisinessLogic.BusinessLogics
{
    public class HelpOrderLogic
    {
        private readonly IOrderLogic orderLogic;

        public HelpOrderLogic(IOrderLogic orderLogic)
        {
            this.orderLogic = orderLogic;
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                ClientId = model.ClientId,
                DateCreate = DateTime.Now,
                OrderSum = model.Sum,
                OrderDishes = model.OrderDishes,
                Status = OrderStatus.Принят
            });
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