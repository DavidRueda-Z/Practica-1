using System;

namespace ejercicio_1
{
    public class EspacioCarro : EspacioParqueo
    {
        private Ticket ticketActual;

        public EspacioCarro(int id, decimal tarifaHora)
        {
            Id = id;
            Tipo = TipoEspacio.Carro;
            TarifaHora = tarifaHora;
            EstaOcupado = false;
        }


        public override void Ocupar(string placaVehiculo, DateTime horaEntrada)
        {
            if (EstaOcupado)
            {
                Console.WriteLine($"El espacio {Id} ya está ocupado.");
                return;
            }


            ticketActual = new Ticket
            {
                Numero = Id, 
                Placa = placaVehiculo,
                HoraEntrada = horaEntrada
            };

            EstaOcupado = true;

            Console.WriteLine($"Vehículo {placaVehiculo} ocupó el espacio {Id} a las {horaEntrada}");
        }


        public override decimal Liberar(DateTime horaSalida)
        {
            if (!EstaOcupado)
            {
                Console.WriteLine($"El espacio {Id} ya está libre.");
                return 0;
            }

            ticketActual.HoraSalida = horaSalida;


            TimeSpan duracion = ticketActual.HoraSalida - ticketActual.HoraEntrada;

            decimal total = ticketActual.CalcularCobro(duracion, TarifaHora, esElectrico: false);

            EstaOcupado = false;

            Console.WriteLine($"Vehículo {ticketActual.Placa} liberó el espacio {Id} a las {horaSalida}. Total a pagar: {total:C}");

            return total;
        }

        public override void MostrarInformacion()
        {
            Console.WriteLine($"Espacio {Id} - Tipo: {Tipo} - Tarifa: {TarifaHora:C} - Ocupado: {EstaOcupado}");

            if (EstaOcupado && ticketActual != null)
            {
                ticketActual.MostrarTicket();
            }
        }
    }
}
