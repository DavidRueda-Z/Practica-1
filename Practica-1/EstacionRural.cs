using System; 
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace RedMeteorologica
{
    public class EstacionRural : Estacion, IAuditable
    {
        private readonly List<Lectura> lecturas = new List<Lectura>();
        private readonly int smoothingWindow = 3;

        private bool ValidarMetadata()
        {
            if (string.IsNullOrWhiteSpace(Codigo) || Codigo.Length < 3 || Codigo.Length > 10)
            {
                Auditar("Error", $"Código inválido (' {Codigo}'). Debe tener entre 3 y 10 caracteres alfanumericos.");
                return false;
            }
            if (!Regex.IsMatch(Codigo, @"^[a-zA-Z0-9]+$"))
            {
                Auditar("Error", $"Código inválido (' {Codigo}'). Solo letras y numeros son permitidos.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Ubicacion) || Ubicacion.Length < 5 || Ubicacion.Length > 50)
            {
                Auditar("Error", $"Ubicación inválida (' {Ubicacion}'). Debe tener entre 5 y 50 caracteres.");
                return false;
            }
            if (!Regex.IsMatch(Ubicacion, @"^[a-zA-Z0-9\s\.,\-]+$"))
            {
                Auditar("Error", $"Ubicación inválida (' {Ubicacion}'). Solo letras, números, espacios y , . - son permitidos.");
                return false;
            }
            return true;
        }
        public override void RegistrarLectura(Lectura nuevaLectura)
        {
            if (!ValidarMetadata())
            {
                return;
            }
            if (nuevaLectura.Temperatura < -50 || nuevaLectura.Temperatura > 60)
            {
                Auditar("Error", $"Temperatura atípica: {nuevaLectura.Temperatura}°C");
                return;
            }
            if (nuevaLectura.Humedad < 0 || nuevaLectura.Humedad > 100)
            {
                Auditar("Error", $"Humedad atípica: {nuevaLectura.Humedad}%");
                return;
            }
            if (nuevaLectura.VelViento < 0 || nuevaLectura.VelViento > 150)
            {
                Auditar("Error", $"Velocidad de viento atípica: {nuevaLectura.VelViento} km/h");
                return;
            }
            if (nuevaLectura.Lluvia < 0 || nuevaLectura.Lluvia > 500)
            {
                Auditar("Error", $"Lluvia atípica: {nuevaLectura.Lluvia} mm");
                return;
            }
            if (nuevaLectura.Presion < 800 || nuevaLectura.Presion > 1100)
            {
                Auditar("Error", $"Presión atípica: {nuevaLectura.Presion} hPa");
                return;
            }
            if (nuevaLectura.Fecha == default) nuevaLectura.Fecha = DateTime.Now;

            var velocidadesPrevias = lecturas
                .Skip(Math.Max(0, lecturas.Count - (smoothingWindow - 1)))
                .Select(l => l.VelViento)
                .ToList();

            double suma = velocidadesPrevias.Sum() + nuevaLectura.VelViento;
            int cuenta = velocidadesPrevias.Count + 1;
            double velSuavizada = Math.Round(suma / cuenta, 2);

            var lecturaARegistrar = nuevaLectura;
            lecturaARegistrar.VelViento = velSuavizada;

            UltimaLectura = lecturaARegistrar;
            lecturas.Add(lecturaARegistrar);

            Auditar("Lectura", $"Lectura registrada en {Codigo}: {lecturaARegistrar.Temperatura}°C, {lecturaARegistrar.Humedad}%, Viento: {lecturaARegistrar.VelViento} km/h, Lluvia: {lecturaARegistrar.Lluvia} mm, Presión: {lecturaARegistrar.Presion} hPa");
        }

        public override void CalcularResumen()
        {
            if (lecturas.Count == 0)
            {
                Auditar("Error", "No hay lecturas para calcular resumen");
                return;
            }

            double tempPromedio = lecturas.Average(l => l.Temperatura);
            double tempMaxima = lecturas.Max(l => l.Temperatura);
            double tempMinima = lecturas.Min(l => l.Temperatura);
            double humedadPromedio = lecturas.Average(l => l.Humedad);
            double lluviaTotal = lecturas.Sum(l => l.Lluvia);
            double velVientoPromedio = lecturas.Average(l => l.VelViento);
            double presionPromedio = lecturas.Average(l => l.Presion);


            Console.WriteLine($"\nResumen estación {Codigo} - {Ubicacion}");
            Console.WriteLine($"Lecturas: {lecturas.Count}");
            Console.WriteLine($"Temp. Prom: {tempPromedio:F1}°C | Máx: {tempMaxima:F1}°C | Mín: {tempMinima:F1}°C");
            Console.WriteLine($"Humedad Prom: {humedadPromedio:F1}%");
            Console.WriteLine($"Lluvia Total: {lluviaTotal:F1} mm");
            Console.WriteLine($"Vel. Viento Prom: {velVientoPromedio:F1} km/h");
            Console.WriteLine($"Presión Prom: {presionPromedio:F1} hPa");

            Auditar("Resumen", $"Resumen calculado correctamente ({lecturas.Count} lecturas)");

        }

        public override void MostrarInformacion()
        {
            string estado = Activo ? "Activa" : "Inactiva";
            Console.WriteLine($"Estación Rural - Código: {Codigo}, Ubicación: {Ubicacion}, Estado: {estado}");
            if (UltimaLectura.Fecha != default)
            {
                Console.WriteLine($"Última Lectura - Fecha: {UltimaLectura.Fecha}, Temp: {UltimaLectura.Temperatura}°C, Humedad: {UltimaLectura.Humedad}%, Viento: {UltimaLectura.VelViento} km/h, Lluvia: {UltimaLectura.Lluvia} mm, Presión: {UltimaLectura.Presion} hPa");
            }
            else
            {
                Console.WriteLine("No hay lecturas registradas.");
            }

        }

        public void Auditar(string evento, string detalle)
        {
            Console.WriteLine($"[AUDITORÍA RURAL] Evento: {evento} - {detalle}");
        }
    }
}
