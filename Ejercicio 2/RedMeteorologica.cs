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
        public void AgregarEstacion()
        {
            String texto = "\nAgregar Estación\nSeleccione el tipo de estación:\n1. Estación Urbana\n2. Estación Rural\nOpción: ";
            Console.WriteLine(texto);
            //Validar que el input sea un número y que sea una opcion valida
            if (!int.TryParse(Console.ReadLine(), out int tipo))
            {
                Console.WriteLine("Error: Debe ingresar un número.");
                return;
            }
            if (tipo != 1 && tipo != 2)
            {
                Console.WriteLine("Tipo de estación no valido");
                return;
            }


            String codigo, ubicacion;
            bool valido = false;
            //Validacion do-while para que el codigo no esté vacio ni sea duplicado
            do
            {
                Console.Write("Código de la estación: ");
                codigo = Console.ReadLine();
                if (string.IsNullOrEmpty(codigo))
                {
                    Console.WriteLine("El código no puede estar vacío.");
                }
                else if (estaciones.Any(e => e.Codigo == codigo))
                {
                    Console.WriteLine("Ya existe una estación con ese código.");
                }
                else
                {
                    valido = true;
                }
            }
            while (!valido);
            //Validacion do-while para que la ubicacion no esté vacia
            do
            {
                valido = false;
                Console.Write("Ubicación: ");
                ubicacion = Console.ReadLine();
                if (string.IsNullOrEmpty(ubicacion))
                {
                    Console.WriteLine("La ubicación no puede estar vacía.");
                }
                else
                {
                    valido = true;
                }
            }
            while (!valido);
            //creamos la nueva estacion
            Estacion nuevaEstacion = null;
            //Se registra la nueva estacion como urbana o rural
            switch (tipo)
            {
                case 1:
                    nuevaEstacion = new EstacionUrbana();
                    nuevaEstacion.Codigo = codigo;
                    nuevaEstacion.Ubicacion = ubicacion;
                    break;
                case 2:
                    nuevaEstacion = new EstacionRural();
                    nuevaEstacion.Codigo = codigo;
                    nuevaEstacion.Ubicacion = ubicacion;
                    break;
            }
            //valida que no este vacia y se agrega a la lista de estaciones
            if (nuevaEstacion == null) return;
            estaciones.Add(nuevaEstacion);

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
