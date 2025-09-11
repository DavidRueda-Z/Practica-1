using System;
using System.Linq;

namespace ejercicio_1
{
    class Program
    {
        static Parqueadero parqueadero = new Parqueadero();

        static void Main(string[] args)
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("==== SISTEMA DE PARQUEADEROS ====");
                Console.WriteLine("1. Registrar entrada de vehículo");
                Console.WriteLine("2. Registrar salida y cobro");
                Console.WriteLine("3. Consultar disponibilidad por tipo");
                Console.WriteLine("4. Mostrar información de todos los espacios");
                Console.WriteLine("5. Mostrar ingresos totales del día");
                Console.WriteLine("6. Salir");
                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarEntradaVehiculo();
                        break;
                    case "2":
                        RegistrarSalidaYCobro();
                        break;
                    case "3":
                        ConsultarDisponibilidadPorTipo();
                        break;
                    case "4":
                        MostrarInformacionEspacios();
                        break;
                    case "5":
                        MostrarIngresosTotales();
                        break;
                    case "6":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida, intente de nuevo.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void RegistrarEntradaVehiculo()
        {
            Console.Clear();
            Console.WriteLine("=== Registrar Entrada ===");

            Console.Write("Ingrese la placa: ");
            string placa = Console.ReadLine();

            Console.WriteLine("Seleccione el tipo de espacio:");
            Console.WriteLine("1. Carro");
            Console.WriteLine("2. Moto");
            Console.WriteLine("3. Discapacitado");
            Console.WriteLine("4. Eléctrico");
            string opcionTipo = Console.ReadLine();

            TipoEspacio tipo = opcionTipo switch
            {
                "1" => TipoEspacio.Carro,
                "2" => TipoEspacio.Moto,
                "3" => TipoEspacio.Discapacitado,
                "4" => TipoEspacio.Electrico,
                _ => TipoEspacio.Carro
            };

            var disponible = parqueadero.DisponiblesPorTipo(tipo).FirstOrDefault();

            if (disponible != null)
            {
                disponible.Ocupar(placa, DateTime.Now);
                parqueadero.Auditar("Entrada", $"Vehículo {placa} entró a espacio {disponible.Id}");
                Console.WriteLine($"Vehículo {placa} registrado en espacio {disponible.Id}");
            }
            else
            {
                Console.WriteLine("No hay espacios disponibles para este tipo.");
            }

            Console.ReadKey();
        }

        static void RegistrarSalidaYCobro()
        {
            Console.Clear();
            Console.WriteLine("=== Registrar Salida ===");

            Console.Write("Ingrese la placa: ");
            string placa = Console.ReadLine();

            var espacio = parqueadero.Espacios.FirstOrDefault(e => e.EstaOcupado && e.PlacaVehiculo == placa);

            if (espacio != null)
            {
                DateTime horaSalida = DateTime.Now;
                Ticket ticket = espacio.Liberar(horaSalida);
                parqueadero.Tickets.Add(ticket);

                Console.WriteLine($"Duración: {ticket.HoraSalida - ticket.HoraEntrada}");
                Console.WriteLine($"Valor a pagar: {ticket.ValorCobrado:C}");

                Console.Write("Ingrese medio de pago: ");
                string medio = Console.ReadLine();
                parqueadero.ProcesarPago(ticket.ValorCobrado, medio);
                parqueadero.Auditar("Salida", $"Vehículo {placa} salió del espacio {espacio.Id}");
            }
            else
            {
                Console.WriteLine("No se encontró vehículo con esa placa.");
            }

            Console.ReadKey();
        }

        static void ConsultarDisponibilidadPorTipo()
        {
            Console.Clear();
            Console.WriteLine("=== Disponibilidad por Tipo ===");

            foreach (TipoEspacio tipo in Enum.GetValues(typeof(TipoEspacio)))
            {
                int cantidad = parqueadero.DisponiblesPorTipo(tipo).Count;
                Console.WriteLine($"{tipo}: {cantidad} espacios disponibles");
            }

            Console.ReadKey();
        }

        static void MostrarInformacionEspacios()
        {
            Console.Clear();
            Console.WriteLine("=== Información de Espacios ===");

            foreach (var espacio in parqueadero.Espacios)
            {
                espacio.MostrarInformacion();
            }

            Console.ReadKey();
        }

        static void MostrarIngresosTotales()
        {
            Console.Clear();
            Console.WriteLine("=== Ingresos Totales ===");
            Console.WriteLine($"Ingresos acumulados: {parqueadero.IngresosTotales():C}");
            Console.ReadKey();
        }
    }
}
