namespace ejercicio_1
{

    public interface IAuditable
    {
        // Registra un evento en el sistema con su detalle
        void Auditar(string evento, string detalle);
    }
}
