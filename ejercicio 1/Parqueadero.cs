using System;
using System.Collections.Generic;

namespace ejercicio_1
{
    // Clase que administra el conjunto de espacios de parqueo
    public class Parqueadero
    {
        //espacios disponibles en el parqueadero
        public List<EspacioParqueo> Espacios { get; set; }

        // tickets generados
        public List<Ticket> Tickets { get; set; }

        // Devuelve los espacios disponibles de un tipo
        public List<EspacioParqueo> DisponiblesPorTipo(TipoEspacio tipo)
        {
            return null;
        }

        // Calcula el total de ingresos del parqueadero
        public decimal IngresosTotales()
        {
            return 0;
        }
    }
}
