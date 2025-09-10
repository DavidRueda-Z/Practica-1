using System;

namespace ejercicio_1
{
    public abstract class EspacioParqueo
    {
        // Atributos comunes
        public int Id { get; set; }
        public TipoEspacio Tipo { get; set; }
        public decimal TarifaHora { get; set; }
        public bool EstaOcupado { get; set; }

        // Recibe la placa y la hora de entrada
        public abstract void Ocupar(string placaVehiculo, DateTime horaEntrada);


        // Recibe la hora de salida y devuelve el valor a pagar
        public abstract decimal Liberar(DateTime horaSalida);

        public abstract void MostrarInformacion();
    }
}
