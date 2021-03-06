﻿using CafeteriaBarnyardBisinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        // Заявки и заказы с расшифровкой по продуктам
        public List<ReportRequestAndOrderProductsViewModel> RequestsAndOrders { get; set; }
    }
}