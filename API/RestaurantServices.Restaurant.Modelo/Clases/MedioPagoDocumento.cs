namespace RestaurantServices.Restaurant.Modelo.Clases
{
    public class MedioPagoDocumento
    {
        public int Id { get; set; }
        public int Monto { get; set; }
        public int IdDocumentoPago { get; set; }
        public int IdMedioPago { get; set; }
        public DocumentoPago DocumentoPago { get; set; }
        public MedioPago MedioPago { get; set; }
    }
}
