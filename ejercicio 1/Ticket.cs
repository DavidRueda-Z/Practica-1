using System;

namespace ejercicio_1
{
    public class Ticket
    {
        public int Numero { get; set; }
        public string Placa { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSalida { get; set; }
        public decimal ValorCobrado { get; set; }

        public decimal CalcularCobro(TimeSpan duracion, decimal tarifaHora, bool esElectrico)
        {
            int horas = (int)Math.Ceiling(duracion.TotalHours);
            decimal total = horas * tarifaHora;

            if (esElectrico)
            {
                total *= 0.8m;
            }

            ValorCobrado = total;
            return total;
        }

        public void MostrarTicket()
        {
            Console.WriteLine($"Ticket #{Numero}");
            Console.WriteLine($"Placa: {Placa}");
            Console.WriteLine($"Entrada: {HoraEntrada}");
            Console.WriteLine($"Salida: {HoraSalida}");
            Console.WriteLine($"Valor cobrado: {ValorCobrado:C}");
        }
    }
}
