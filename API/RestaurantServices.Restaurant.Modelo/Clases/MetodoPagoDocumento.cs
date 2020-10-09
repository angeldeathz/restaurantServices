namespace RestaurantServices.Restaurant.Modelo.Clases
{
    public class MetodoPagoDocumento
    {
        public int Id { get; set; }
        public int Monto { get; set; }
        public int IdDocumentoPago { get; set; }
        public int IdMetodoPago { get; set; }
        public DocumentoPago DocumentoPago { get; set; }
        public MedioPago MetodoPago { get; set; }
    }
}
