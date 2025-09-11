using System;
using System.Collections.Generic;

namespace ejercicio_1
{
    // Clase que administra el conjunto de espacios de parqueo
    public class Parqueadero : IPagable, IAuditable
    {
        //espacios disponibles en el parqueadero
        public List<EspacioParqueo> Espacios { get; set; } = new List<EspacioParqueo>();

        // tickets generados
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        //constructor
        public Parqueadero()
        {
            //inicializo algunos espacios
            Espacios.Add(new EspacioCarro(1, 5000));
            Espacios.Add(new EspacioCarro(2, 5000));
            Espacios.Add(new EspacioCarro(3, 3000));

        }

        // Devuelve los espacios disponibles de un tipo
        public List<EspacioParqueo> DisponiblesPorTipo(TipoEspacio tipo)
        {
            return Espacios.Where(e => e.Tipo == tipo && ! e.EstaOcupado).ToList();
        }

        // Calcula el total de ingresos del parqueadero
        public decimal IngresosTotales()
        {
            return Tickets.sum(t => t.ValorCobrado);
        }

        // Implementación de IPagable
        public void ProcesarPago(decimal valor, string medio)
        {
            // Lógica para procesar el pago
            Console.WriteLine($"Procesando pago de {valor:C} vía {medio}");
        }

        // Implementación de IAuditable
        public void Auditar(string evento, string detalle)
        {
            // Lógica para registrar el evento
            Console.WriteLine($"[AUDITORÍA] Evento: {evento}, Detalle: {detalle}");
        }
    }
}
