﻿using System;

namespace CafeteriaBarnyardBisinessLogic.BindingModels
{
    public class ReportPeriodBindingModel
    {
        public string FileName { get; set; }

        public int ClientId { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}
