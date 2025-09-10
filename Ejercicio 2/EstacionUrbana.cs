using System;
using System.Collections.Generic;
using System.Linq;

namespace RedMeteorologica
{
    public class EstacionUrbana : Estacion, IAuditable
    {
        private List<Lectura> lecturas = new List<Lectura>();

        public override void RegistrarLectura(Lectura nuevaLectura)
        {
            if (nuevaLectura.Temperatura < -50 || nuevaLectura.Temperatura > 60 ||
                nuevaLectura.Humedad < 0 || nuevaLectura.Humedad > 100)
            {
                Auditar("Error", $"Dato atípico: {nuevaLectura.Temperatura}°C, {nuevaLectura.Humedad}%");
                return;
            }

            UltimaLectura = nuevaLectura;
            lecturas.Add(nuevaLectura);

            Auditar("Lectura", $"Lectura registrada en {Codigo}: {nuevaLectura.Temperatura}°C, {nuevaLectura.Humedad}%");
        }

        public override void CalcularResumen()
        {
            if (lecturas.Count == 0)
            {
                Auditar("Error, no hay lecturas para calcular resumen");
                return;
            }

            double tempPromedio = lecturas.Average(l => l.Temperatura);
            double tempMaxima = lecturas.Max(l => l.Temperatura);
            double tempMinima = lecturas.Min(l => l.Temperatura);

            Console.WriteLine($"\nResumen estación {Codigo} - {Ubicacion}");
            Console.WriteLine($"Promedio Temp: {tempPromedio:F1}°C | Máx: {tempMaxima:F1}°C | Mín: {tempMinima:F1}°C");

            Auditar("Resumen", "Resumen calculado correctamente");
        }

        public override void MostrarInformacion()
        {
            string estado = Activo ? "Activa" : "Inactiva";
            Console.WriteLine($"Estación Urbana - Código: {Codigo}, Ubicación: {Ubicacion}, Estado: {estado}");
        }

        public void Auditar(string evento, string detalle)
        {
            Console.WriteLine($"[AUDITORÍA DE METEOROLOGÍA] Evento: {evento} - {detalle}");
        }
    }
}
