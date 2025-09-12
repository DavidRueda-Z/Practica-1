using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaParqueadero
{
    public class Parqueadero : IPagable, IAuditable
    {
        public List<EspacioParqueo> Espacios { get; set; } = new List<EspacioParqueo>();
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        public Parqueadero()
        {
            Espacios.Add(new EspacioCarro(1, 5000));
            Espacios.Add(new EspacioCarro(2, 5000));
            Espacios.Add(new EspacioCarro(3, 3000));
        }

        public List<EspacioParqueo> DisponiblesPorTipo(TipoEspacio tipo)
        {
            return Espacios.Where(e => e.Tipo == tipo && !e.EstaOcupado).ToList();
        }

        public decimal IngresosTotales()
        {
            return Tickets.Sum(t => t.ValorCobrado);
        }

        public void ProcesarPago(decimal valor, string medio)
        {
            Console.WriteLine($"Procesando pago de {valor:C} vía {medio}");
        }

        public void Auditar(string evento, string detalle)
        {
            Console.WriteLine($"[AUDITORÍA] Evento: {evento}, Detalle: {detalle}");
        }
    }
}
