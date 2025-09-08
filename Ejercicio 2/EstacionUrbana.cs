namespace RedMeteorologica
{
    public class EstacionUrbana : Estacion, IAuditable
    {
        public override void RegistrarLectura(Lectura nuevaLectura)
        {
            // TODO: Implementar lógica (filtrar datos atípicos)
        }

        public override void CalcularResumen()
        {
            // TODO: Implementar cálculo de resumen
        }

        public override void MostrarInformacion()
        {
            // TODO: Mostrar información de la estación
        }

        public void Auditar(string evento, string detalle)
        {
            // TODO: Registrar auditoría
        }
    }
}
