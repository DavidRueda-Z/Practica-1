using System;

namespace ejercicio_1
{
    public abstract class EspacioParqueo
    {
        // Atributos
        public int Id { get; protected set; }
        public TipoEspacio Tipo { get; protected set; }
        public decimal TarifaHora { get; protected set; }
        public bool EstaOcupado { get; protected set; }

        // Métodos abstractos
        public abstract void Ocupar(string placaVehiculo, DateTime horaEntrada);
        public abstract decimal Liberar(DateTime horaSalida);
        public abstract void MostrarInformacion();
    }
}
