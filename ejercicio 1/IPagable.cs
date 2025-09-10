namespace ejercicio_1
{
    // Interface que define operaciones de pago
    public interface IPagable
    {
        // Procesa el pago con el valor y el medio de pago (efectivo, tarjeta, etc.)
        void ProcesarPago(decimal valor, string medio);
    }
}
