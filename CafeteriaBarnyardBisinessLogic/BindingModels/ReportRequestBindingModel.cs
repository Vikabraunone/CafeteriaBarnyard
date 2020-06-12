using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.BindingModels
{
    /// <summary>
    /// Отчет по заявке на продукты
    /// </summary>
    public class ReportRequestBindingModel
    {
        public string FileName { get; set; }

        public int ClientId { get; set; }

        public int AdminId { get; set; }

        public Dictionary<int, (string, double)> Request { get; set; }
    }
}
