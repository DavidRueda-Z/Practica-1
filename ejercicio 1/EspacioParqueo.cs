using System;

namespace ejercicio_1
{

    public abstract class EspacioParqueo
    {

        public int Id { get; set; }
        public TipoEspacio Tipo { get; set; }
        public decimal TarifaHora { get; set; }
        public bool EstaOcupado { get; set; }

        public abstract void Ocupar(string placaVehiculo, DateTime horaEntrada);
        public abstract decimal Liberar(DateTime horaSalida);
        public abstract void MostrarInformacion();
    }
}
