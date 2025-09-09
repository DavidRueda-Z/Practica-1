using System.Collections.Generic;
using System.Linq;

namespace RedMeteorologica
{
    public class RedMeteorologica
    {
        private readonly List<Estacion> estaciones;

        public RedMeteorologica()
        {
            estaciones = new List<Estacion>();
        }

        /// <summary>
        /// Agrega una estación a la red si no es nula ni duplicada.
        /// </summary>
        /// <param name="e">La estación a agregar.</param>
        public void AgregarEstacion(Estacion e)
        {
            if (e == null) return;
            if (!estaciones.Contains(e))
                estaciones.Add(e);
        }

        /// <summary>
        /// Devuelve una copia de la lista de estaciones.
        /// </summary>
        public IReadOnlyList<Estacion> ObtenerEstaciones()
        {
            if (estaciones.Count == 0)
            {
                Console.WriteLine("No hay estaciones registradas.");
            }

            Console.WriteLine("\nLista de Estaciones");

            int cont = 0;
            foreach (var estacion in estaciones)
            {
                cont++;
                Console.WriteLine("Estacion #" + cont + "\nCodigo: " + estacion.Codigo + "\nUbicacion: " + estacion.Ubicacion + "\n");
            }

            return estaciones.AsReadOnly();
        }

        /// <summary>
        /// Calcula y muestra un resumen global de las estaciones.
        /// </summary>
        public void ResumenGlobal()
        {
            if (estaciones.Count == 0)
            {
                System.Console.WriteLine("No hay estaciones registradas.");
                return;
            }

            // Ejemplo: calcular promedio de temperatura si Estacion tiene una propiedad Temperatura
            // double promedio = estaciones.Average(e => e.Temperatura);
            // System.Console.WriteLine($"Temperatura promedio: {promedio}");

            System.Console.WriteLine($"Total de estaciones: {estaciones.Count}");
        }
    }
}
