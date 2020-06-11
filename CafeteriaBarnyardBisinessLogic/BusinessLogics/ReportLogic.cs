using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.HelperModels;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IRequestLogic requestLogic;

        private readonly IOrderLogic orderLogic;

        private readonly IClientLogic clientLogic;

        private readonly IProductLogic productLogic;

        private readonly IDishLogic dishLogic;

        public ReportLogic(IRequestLogic requestLogic, IOrderLogic orderLogic,
            IProductLogic productLogic, IDishLogic dishLogic, IClientLogic clientLogic)
        {
            this.requestLogic = requestLogic;
            this.orderLogic = orderLogic;
            this.productLogic = productLogic;
            this.dishLogic = dishLogic;
            this.clientLogic = clientLogic;
        }

        /// <summary>
        /// Отправка заявки по почте файлом Word
        /// </summary>
        /// <param name="model"></param>
        public void SendRequestToWord(ReportRequestBindingModel model)
        {
            model.FileName = "C:\\Users\\User\\Downloads\\tempBarnyard.docx";
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Заявка на продукты",
                Request = GetRequest(model)
            });
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel { Id = model.AdminId })?[0]?.Email,
                Subject = $"Заявка на продукты",
                Text = $"Заявка от клиента {model.ClientId}."
            },
                model.FileName);
        }

        /// <summary>
        /// Отправка заявки по почте файлом Excel
        /// </summary>
        /// <param name="model"></param>
        public void SendRequestToExcelFile(ReportRequestBindingModel model)
        {
            model.FileName = "C:\\Users\\User\\Downloads\\tempBarnyard.xlsx";
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Заявка на продукты",
                Request = GetRequest(model)
            });
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel { Id = model.AdminId })?[0]?.Email,
                Subject = $"Заявка на продукты",
                Text = $"Заявка от клиента {model.ClientId}."
            },
                model.FileName);
        }

        /// <summary>
        /// Отправка отчета по заявкам и заказам с расшифровкой по продуктам
        /// </summary>
        /// <param name="model"></param>
        public void SendReriodRequestsAndOrdersToPdf(ReportPeriodBindingModel model)
        {
            model.FileName = "C:\\Users\\User\\Downloads\\tempBarnyard.pdf";
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Заявка на продукты",
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                RequestsAndOrders = GetRequestOrderProducts(model)
            });
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel { Id = model.ClientId })?[0]?.Email,
                Subject = $"Отчет по заявкам и заказам за период с расшифровкой по продуктам",
                Text = $"Отчет за период с {model.DateFrom} по {model.DateTo}."
            },
                model.FileName);
        }

        public List<ReportRequestViewModel> GetRequest(ReportRequestBindingModel model)
        {
            var list = new List<ReportRequestViewModel>();
            foreach (var e in model.Request)
                list.Add(new ReportRequestViewModel { ProductName = e.Value.Item1, Weight = e.Value.Item2 });
            return list;
        }

        /// <summary>
        /// Получение списка заявок и заказов с расшифровкой по продуктам
        /// </summary>
        /// <returns></returns>
        public List<ReportRequestOrderProductsViewModel> GetRequestOrderProducts(ReportPeriodBindingModel model)
        {
            var reportList = new List<ReportRequestOrderProductsViewModel>();
            var requests = requestLogic.Read(new RequestBindingModel { DateFrom = model.DateFrom, DateTo = model.DateTo });
            var orders = orderLogic.Read(new OrderBindingModel { DateFrom = model.DateFrom, DateTo = model.DateTo });
            int ri = 0;
            int oi = 0;
            while (ri < requests.Count && oi < orders.Count)
            {
                if (requests[ri].DateRequest < orders[oi].DateCreate)
                {
                    foreach (var product in requests[ri].RequestProducts)
                    {
                        reportList.Add(new ReportRequestOrderProductsViewModel
                        {
                            Id = requests[ri].Id.Value,
                            DateCreate = requests[ri].DateRequest,
                            DishName = string.Empty,
                            ProductName = product.Value.Item1,
                            Count = product.Value.Item2
                        });
                    }
                    ri++;
                }
                else
                {
                    foreach (var dish in orders[oi].OrderDishes)
                    {
                        var currentDish = dishLogic.Read(new DishBindingModel { Id = dish.Key })[0];
                        foreach (var product in currentDish.DishProducts)
                            reportList.Add(new ReportRequestOrderProductsViewModel
                            {
                                Id = null,
                                DateCreate = orders[oi].DateCreate,
                                DishName = currentDish.DishName,
                                ProductName = product.Value.Item1,
                                Count = product.Value.Item2
                            });
                    }
                    oi++;
                }
            }
            while (ri < requests.Count)
            {
                foreach (var product in requests[ri].RequestProducts)
                {
                    reportList.Add(new ReportRequestOrderProductsViewModel
                    {
                        Id = requests[ri].Id.Value,
                        DateCreate = requests[ri].DateRequest,
                        DishName = string.Empty,
                        ProductName = product.Value.Item1,
                        Count = product.Value.Item2
                    });
                }
                ri++;
            }
            while (oi < orders.Count)
            {
                foreach (var dish in orders[oi].OrderDishes)
                {
                    var currentDish = dishLogic.Read(new DishBindingModel { Id = dish.Key })[0];
                    foreach (var product in currentDish.DishProducts)
                        reportList.Add(new ReportRequestOrderProductsViewModel
                        {
                            Id = null,
                            DateCreate = orders[oi].DateCreate,
                            DishName = currentDish.DishName,
                            ProductName = product.Value.Item1,
                            Count = product.Value.Item2
                        });
                }
                oi++;
            }
            return reportList;
        }
    }
}
