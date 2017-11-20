namespace Fanex.Result.Core.TestUtils
{
    using System;
    using System.IO;

    public static class TemplateFileUtils
    {
        public static string GetHtmlContentFromTestTemplate(string fileName)
            => RemoveEndLineSymbolHtml(File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}/../../TestTemplate/html/{fileName}"));

        public static string RemoveEndLineSymbolHtml(string htmlText)
          => htmlText.Replace("\r", string.Empty).Replace(" ", string.Empty).Replace("\n", string.Empty);

        public static string GetExcelTemplatePath(string fileName)
            => $"{AppDomain.CurrentDomain.BaseDirectory}/../../TestTemplate/excel/{fileName}";

        public static string GetExcelTemplateFolderPath()
            => $"{AppDomain.CurrentDomain.BaseDirectory}/../../TestTemplate/excel/";
    }
}