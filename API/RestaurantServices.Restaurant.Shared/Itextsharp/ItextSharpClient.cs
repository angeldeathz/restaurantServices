using System;
using System.IO;
using iText.Html2pdf;

namespace RestaurantServices.Restaurant.Shared.Itextsharp
{
    public class ItextSharpClient
    {
        public string CreatePdf(string html, string fileName)
        {
            var path = CrearCarpetaStorage();
            path = $"{path}\\{fileName}";

            var file = new FileStream(path, FileMode.Create);
            HtmlConverter.ConvertToPdf(html, file);
            file.Close();

            CreatePdfBase64(html);

            return path;
        }

        public string CreatePdfBase64(string html)
        {
            string base64;

            using (var msOutput = new MemoryStream())
            {
                HtmlConverter.ConvertToPdf(html, msOutput);
                var bytes = msOutput.ToArray();
                base64 = Convert.ToBase64String(bytes);
            }
            return base64;
        }

        public string CrearCarpetaStorage()
        {
            var path = "C:\\Storage";
            var exists = Directory.Exists(path);

            if (!exists)
                Directory.CreateDirectory(path);

            return path;
        }
    }
}
