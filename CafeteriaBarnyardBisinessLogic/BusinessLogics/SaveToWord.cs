﻿using CafeteriaBarnyardBisinessLogic.HelperModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;

namespace CafeteriaBarnyardBisinessLogic.BusinessLogics
{
    static class SaveToWord
    {
        /// <summary>
        /// Создание документа
        /// </summary>
        /// <param name="info"></param>
        public static void CreateDoc(WordInfo info)
        {
            using (WordprocessingDocument wordDocument =
                WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<string> { info.Title },
                    TextProperties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                foreach (var product in info.Request)
                {
                    docBody.AppendChild(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<string> { product.ProductName, product.Weight.ToString() },
                        TextProperties = new WordParagraphProperties
                        {
                            Size = "24",
                            JustificationValues = JustificationValues.Both
                        }
                    }));
                }
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        /// <summary>
        /// Настройки страницы
        /// </summary>
        /// <returns></returns>
        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize { Orient = PageOrientationValues.Portrait };
            properties.AppendChild(pageSize);
            return properties;
        }

        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph == null) return null;
            Paragraph docParagraph = new Paragraph();
            docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
            for (int i = 0; i < paragraph.Texts.Count; i++)
            {
                Run docRun = new Run();
                RunProperties properties = new RunProperties();
                properties.AppendChild(new FontSize { Val = paragraph.TextProperties.Size });
                if (paragraph.TextProperties.Bold)
                    properties.AppendChild(new Bold());
                docRun.AppendChild(properties);
                docRun.AppendChild(new Text { Text = " " + paragraph.Texts[i], Space = SpaceProcessingModeValues.Preserve });
                docParagraph.AppendChild(docRun);
            }
            return docParagraph;
        }

        private static Run CreateBoldText(WordParagraph paragraph, int index)
        {
            Run docRun = new Run();
            RunProperties properties = new RunProperties();
            properties.AppendChild(new FontSize { Val = paragraph.TextProperties.Size });
            properties.AppendChild(new Bold());
            docRun.AppendChild(properties);
            docRun.AppendChild(new Text { Text = paragraph.Texts[index], Space = SpaceProcessingModeValues.Preserve });
            return docRun;
        }

        /// <summary>
        /// Задание форматирования для абзаца
        /// </summary>
        /// <param name="paragraphProperties"></param>
        /// <returns></returns>
        private static ParagraphProperties CreateParagraphProperties(WordParagraphProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification() { Val = paragraphProperties.JustificationValues });
                properties.AppendChild(new SpacingBetweenLines { LineRule = LineSpacingRuleValues.Auto });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                    paragraphMarkRunProperties.AppendChild(new FontSize { Val = paragraphProperties.Size });
                if (paragraphProperties.Bold)
                    paragraphMarkRunProperties.AppendChild(new Bold());
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
    }
}
