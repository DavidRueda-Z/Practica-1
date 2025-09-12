using System;

namespace SistemaParqueadero
{
    public abstract class EspacioParqueo
    {
        public int Id { get; protected set; }
        public TipoEspacio Tipo { get; protected set; }
        public decimal TarifaHora { get; protected set; }
        public bool EstaOcupado { get; protected set; }
        public string PlacaVehiculo => ticketActual?.Placa;

        protected Ticket ticketActual;

        public abstract void Ocupar(string placaVehiculo, DateTime horaEntrada);
        public abstract Ticket Liberar(DateTime horaSalida);
        public abstract void MostrarInformacion();
    }
}
