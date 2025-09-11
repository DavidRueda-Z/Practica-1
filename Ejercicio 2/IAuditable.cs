namespace RedMeteorologica
{
    public interface IAuditable
    {
        void Auditar(string evento, string detalle);
    }
}