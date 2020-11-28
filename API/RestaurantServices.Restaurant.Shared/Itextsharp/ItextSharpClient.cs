﻿using System.IO;
using iText.Html2pdf;

namespace RestaurantServices.Restaurant.Shared.Itextsharp
{
    public class ItextSharpClient
    {
//        public string CreatePdf2(string html, string fileName)
//        {
//            var path = CrearCarpetaStorage();
//            path = $"{path}\\{fileName}";

//            var file = new FileStream(path, FileMode.Create);
//            var document = new Document();
//            PdfWriter.GetInstance(document, file);
//            document.Open();
//#pragma warning disable 612
//            var hw = new HTMLWorker(document);
//#pragma warning restore 612
//            hw.Parse(new StringReader(html));
//            document.Close();
//            return path;
//        }

        public string CreatePdf(string html, string fileName)
        {
            var path = CrearCarpetaStorage();
            path = $"{path}\\{fileName}";

            var file = new FileStream(path, FileMode.Create);
            HtmlConverter.ConvertToPdf(html, file);
            file.Close();

            return path;
        }

//        public string CreatePdfBase64(string html)
//        {
//            string base64;

//            using (var msOutput = new MemoryStream())
//            {
//                var reader = new StringReader(html);
//                var document = new Document(PageSize.A4, 30, 30, 30, 30);
//                PdfWriter.GetInstance(document, msOutput);

//#pragma warning disable 612
//                var worker = new HTMLWorker(document);
//#pragma warning restore 612

//                document.Open();
//                worker.StartDocument();

//                worker.Parse(reader);
//                worker.EndDocument();
//                worker.Close();
//                document.Close();

//                var bytes = msOutput.ToArray();
//                base64 = Convert.ToBase64String(bytes);
//                document.Close();
//            }

//            return base64;
//        }

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
