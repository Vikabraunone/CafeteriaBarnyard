using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.HelperModels
{
    /// <summary>
    /// Информация по каждому абзацу
    /// </summary>
    class WordParagraph
    {
        public List<string> Texts { get; set; }

        public WordParagraphProperties TextProperties { get; set; }
    }
}
