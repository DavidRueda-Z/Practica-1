namespace SistemaParqueadero
{

    public interface IAuditable
    {
        // Registra un evento en el sistema con su detalle
        void Auditar(string evento, string detalle);
    }
}
namespace RedMeteorologica
{
    public interface IAuditable
    {
        void Auditar(string evento, string detalle);
    }
}
