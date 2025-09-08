using System;

namespace RedMeteorologica
{
    public abstract class Estacion
    {
        public string Codigo { get; set; }
        public string Ubicacion { get; set; }
        public Lectura UltimaLectura { get; set; }
        public bool Activo { get; set; }

        public abstract void RegistrarLectura(Lectura nuevaLectura);
        public abstract void CalcularResumen();
        public abstract void MostrarInformacion();
    }
}
