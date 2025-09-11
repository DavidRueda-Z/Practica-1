using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RedMeteorologica
{
    public class EstacionUrbana : Estacion, IAuditable
    {
        private List<Lectura> lecturas = new List<Lectura>();

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
                Auditar("Error", "no hay lecturas para calcular resumen");
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
