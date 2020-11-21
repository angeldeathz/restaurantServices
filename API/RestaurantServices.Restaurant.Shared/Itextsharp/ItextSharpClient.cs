using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace RestaurantServices.Restaurant.Shared.Itextsharp
{
    public class ItextSharpClient
    {
        public string CreatePdf(string html, string fileName)
        {
            var path = CrearCarpetaStorage();
            path = $"{path}\\{fileName}";

            var file = new FileStream(path, FileMode.Create);
            var document = new Document();
            PdfWriter.GetInstance(document, file);
            document.Open();
#pragma warning disable 612
            var hw = new HTMLWorker(document);
#pragma warning restore 612
            hw.Parse(new StringReader(html));
            document.Close();
            return path;
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
