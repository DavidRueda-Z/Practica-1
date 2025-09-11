using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions; 

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
                if (string.IsNullOrWhiteSpace(codigo) || codigo.Length < 3 || codigo.Length > 10)
                {
                    Console.WriteLine($"Código inválido (' {codigo}'). Debe tener entre 3 y 10 caracteres alfanumericos.");
                }
                else if (!Regex.IsMatch(codigo, @"^[a-zA-Z0-9]+$"))
                {
                    Console.WriteLine($"Código inválido (' {codigo}'). Solo letras y numeros son permitidos.");
                }
                else if (string.IsNullOrEmpty(codigo))
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
                else if (string.IsNullOrWhiteSpace(ubicacion) || ubicacion.Length < 5 || ubicacion.Length > 50)
                {
                    Console.WriteLine($"Ubicación inválida (' {ubicacion}'). Debe tener entre 5 y 50 caracteres.");
                }
                else if (!Regex.IsMatch(ubicacion, @"^[a-zA-Z0-9\s\.,\-]+$"))
                {
                    Console.WriteLine($"Ubicación inválida (' {ubicacion}'). Solo letras, números, espacios y , . - son permitidos.");
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

            return estaciones.AsReadOnly();
        }

     
        public void ResumenGlobal()
        {
            if (estaciones.Count == 0)
            {
                Console.WriteLine(" No hay estaciones registradas.");
                return;
            }

            // Filtrar estaciones que tengan lecturas
            var estacionesConLectura = estaciones.Where(e => e.UltimaLectura.Fecha != default).ToList();

            if (estacionesConLectura.Count == 0)
            {
                Console.WriteLine("No hay lecturas registradas en ninguna estación.");
                return;
            }

            // Calcular promedios globales
            double tempPromedio = estacionesConLectura.Average(e => e.UltimaLectura.Temperatura);
            double humedadProm = estacionesConLectura.Average(e => e.UltimaLectura.Humedad);
            double vientoProm = estacionesConLectura.Average(e => e.UltimaLectura.VelViento);
            double lluviaProm = estacionesConLectura.Average(e => e.UltimaLectura.Lluvia);
            double presionProm = estacionesConLectura.Average(e => e.UltimaLectura.Presion);

            Console.WriteLine("\n===== RESUMEN GLOBAL =====");
            Console.WriteLine($"Total de estaciones registradas: {estaciones.Count}");
            Console.WriteLine($"Estaciones con lecturas: {estacionesConLectura.Count}");
            Console.WriteLine($"Temp. promedio: {tempPromedio:F1} °C");
            Console.WriteLine($"Humedad promedio: {humedadProm:F1} %");
            Console.WriteLine($"Viento promedio: {vientoProm:F1} m/s");
            Console.WriteLine($"Lluvia promedio: {lluviaProm:F1} mm");
            Console.WriteLine($"Presión promedio: {presionProm:F1} hPa");
        }

    }
}
