using CafeteriaBarnyardBisinessLogic.ViewModels;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportRequestViewModel> Request { get; set; }
    }
}