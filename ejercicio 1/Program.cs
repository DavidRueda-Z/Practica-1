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
				Console.WriteLine("1. Registrar entrada de veh�culo");
				Console.WriteLine("2. Registrar salida y cobro");
				Console.WriteLine("3. Consultar disponibilidad por tipo");
				Console.WriteLine("4. Mostrar informaci�n de todos los espacios");
				Console.WriteLine("5. Mostrar ingresos totales del d�a");
				Console.Write("\nSeleccione una opci�n: ");

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
						Console.WriteLine("Opci�n no v�lida, intente de nuevo.");
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
			Console.WriteLine("[Registrar Salida] Aqu� se liberar� un espacio, se generar� ticket y se procesar� el pago.");
		}

		static void ConsultarDisponibilidadPorTipo()
		{
			Console.WriteLine("[Disponibilidad] Aqu� se mostrar� cu�ntos espacios hay disponibles por tipo.");
		}

		static void MostrarInformacionEspacios()
		{
			Console.WriteLine("[Informaci�n Espacios] Aqu� se listar� cada espacio con su estado.");
		}

		static void MostrarIngresosTotales()
		{
			Console.WriteLine("[Ingresos] Aqu� se mostrar� la suma de todos los ingresos del d�a.");
		}
	}
}
