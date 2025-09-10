using System;

namespace ejercicio_1
{
    // Clase que representa el ticket de entrada y salida de un vehiculo
    public class Ticket
    {
        // Atributos basicos del ticket
        public int Numero { get; set; }
        public string Placa { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSalida { get; set; }
        public decimal ValorCobrado { get; set; }

        // Metodo para calcular el valor a cobrar
        // Recibe la duracion de la estancia, la tarifa y si el vehiculo es electrico
        // Devuelve el total a pagar
        public decimal CalcularCobro(TimeSpan duracion, decimal tarifaHora, bool esElectrico)
        {
            // Aqui se calcular el cobro segun las reglas
            return 0;
        }
    }
}
