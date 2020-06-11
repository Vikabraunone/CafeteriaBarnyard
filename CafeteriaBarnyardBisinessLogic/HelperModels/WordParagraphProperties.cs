using DocumentFormat.OpenXml.Wordprocessing;

namespace CafeteriaBarnyardBisinessLogic.HelperModels
{
    /// <summary>
    /// Информация для форматирования текста абзаца
    /// </summary>
    class WordParagraphProperties
    {
        public string Size { get; set; }

        public bool Bold { get; set; }

        public JustificationValues JustificationValues { get; set; }
    }
}