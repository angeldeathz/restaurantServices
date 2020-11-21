using System.Collections.Generic;

namespace RestaurantServices.Restaurant.Shared.Mail
{
    public class Email
    {
        public string ReceptorEmail { get; set; }
        public string ReceptorNombre { get; set; }
        public string Asunto { get; set; }
        public string Contenido { get; set; }
        public List<string> UrlAdjunto { get; set; }
    }
}
