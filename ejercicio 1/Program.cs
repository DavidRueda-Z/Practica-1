using System;

namespace ejercicio_1
{
	class Program
	{
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
						break;
				}
			}
		}

		static void RegistrarEntradaVehiculo()
		{
			Console.Clear();
            Console.WriteLine("[Registrar Entrada] Ingrese la placa del vehiculo:.");
			string
		}

		static void RegistrarSalidaYCobro()
		{
			Console.WriteLine("[Registrar Salida] Aquí se liberará un espacio, se generará ticket y se procesará el pago.");
		}

		static void ConsultarDisponibilidadPorTipo()
		{
			Console.WriteLine("[Disponibilidad] Aquí se mostrará cuántos espacios hay disponibles por tipo.");
		}

		static void MostrarInformacionEspacios()
		{
			Console.WriteLine("[Información Espacios] Aquí se listará cada espacio con su estado.");
		}

		static void MostrarIngresosTotales()
		{
			Console.WriteLine("[Ingresos] Aquí se mostrará la suma de todos los ingresos del día.");
		}
	}
}
