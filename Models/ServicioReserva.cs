namespace WebApiFinal.Models
{
    public class ServicioReserva
    {
        public int ServicioId { get; set; }
        public Servicio Servicio  { get; set; }

        public int ReservaId { get; set; }
        public Reserva Reserva { get; set; }
    }
}
