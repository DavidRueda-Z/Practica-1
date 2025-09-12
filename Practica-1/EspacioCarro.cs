using System;

namespace SistemaParqueadero
{
    public class EspacioCarro : EspacioParqueo
    {
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

        public override Ticket Liberar(DateTime horaSalida)
        {
            if (!EstaOcupado)
            {
                Console.WriteLine($"El espacio {Id} ya está libre.");
                return null;
            }

            ticketActual.HoraSalida = horaSalida;
            TimeSpan duracion = ticketActual.HoraSalida - ticketActual.HoraEntrada;

            ticketActual.CalcularCobro(duracion, TarifaHora, false);
            EstaOcupado = false;

            Console.WriteLine($"Vehículo {ticketActual.Placa} liberó el espacio {Id} a las {horaSalida}. Total a pagar: {ticketActual.ValorCobrado:C}");
            ticketActual.Placa = null;
            return ticketActual;
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
