using System;
using SistemaParqueadero;
using RedMeteorologica;

class Program
{
    static RedMeteorologica.RedMeteorologica red = new RedMeteorologica.RedMeteorologica();
    static void Main(string[] args)
    {
        MostrarMenuPrincipal();
    }

    static void MostrarMenuPrincipal()
    {
        string[] opciones = {
            "Sistema de Parqueaderos",
            "Red de Estaciones Meteorológicas",
            "Salir"
        };

        bool salir = false;
        while (!salir)
        {
            int opcion = MostrarMenu("MENÚ PRINCIPAL", opciones);
            switch (opcion)
            {
                case 1:
                    MenuParqueadero();
                    break;
                case 2:
                    MenuMeteorologia();
                    break;
                case 3:
                    salir = true;
                    break;
            }
        }
    }

    static void MenuParqueadero()
    {
        string[] opciones = {
            "Registrar entrada de vehículo",
            "Registrar salida y cobro",
            "Consultar disponibilidad por tipo",
            "Mostrar información de todos los espacios",
            "Mostrar ingresos totales del día",
            "Volver"
        };

        bool volver = false;
        while (!volver)
        {
            int opcion = MostrarMenu("Sistema de Parqueaderos", opciones);
            switch (opcion)
            {
                case 1:
                    // TODO: Registrar entrada
                    break;
                case 2:
                    // TODO: Registrar salida
                    break;
                case 3:
                    // TODO: Consultar disponibilidad
                    break;
                case 4:
                    // TODO: Mostrar información
                    break;
                case 5:
                    // TODO: Mostrar ingresos
                    break;
                case 6:
                    volver = true;
                    break;
            }
        }
    }

    static void MenuMeteorologia()
    {
        string[] opciones = {
            "Agregar estación",
            "Registrar lectura en una estación",
            "Ver resumen por estación",
            "Ver resumen global",
            "Listar estaciones",
            "Volver"
        };

        bool volver = false;
        while (!volver)
        {
            int opcion = MostrarMenu("Red de Estaciones Meteorológicas", opciones);
            switch (opcion)
            {
                case 1:
                    Console.Write("Ingrese el código de la estación: ");
                    string cod = Console.ReadLine();
                    Console.Write("Ingrese la ubicación: ");
                    string ubicacion = Console.ReadLine();
                    Console.Write("Tipo (1=Urbana, 2=Rural): ");
                    int tipo = int.Parse(Console.ReadLine());

                    Estacion nuevaEstacion;
                    if (tipo == 1)
                        nuevaEstacion = new EstacionUrbana { Codigo = cod, Ubicacion = ubicacion, Activo = true };
                    else
                        nuevaEstacion = new EstacionRural { Codigo = cod, Ubicacion = ubicacion, Activo = true };

                    red.AgregarEstacion(nuevaEstacion);
                    Console.WriteLine("✅ Estación agregada correctamente.");
                    break;
                case 2:
                    Console.Write("Ingrese el código de la estación: ");
                    string codigo = Console.ReadLine();

                    // Buscar estación en la red
                    var estacion = red.ObtenerEstaciones()
                                      .FirstOrDefault(e => e.Codigo == codigo);

                    if (estacion == null)
                    {
                        Console.WriteLine("Estación no encontrada.");
                        break;
                    }

                    // Pedir datos de la lectura
                    Lectura nueva = new Lectura();

                    Console.Write("Temperatura (°C): ");
                    nueva.Temperatura = double.Parse(Console.ReadLine());

                    Console.Write("Humedad (%): ");
                    nueva.Humedad = double.Parse(Console.ReadLine());

                    Console.Write("Velocidad del viento (m/s): ");
                    nueva.VelViento = double.Parse(Console.ReadLine());

                    Console.Write("Lluvia (mm): ");
                    nueva.Lluvia = double.Parse(Console.ReadLine());

                    Console.Write("Presión (hPa): ");
                    nueva.Presion = double.Parse(Console.ReadLine());

                    nueva.Fecha = DateTime.Now;

                    // Registrar lectura
                    estacion.RegistrarLectura(nueva);
                    Console.WriteLine("Lectura registrada correctamente.");

                    break;
                case 3:
                    Console.Write("Ingrese el código de la estación: ");
                    string codResumen = Console.ReadLine();

                    var estacionResumen = red.ObtenerEstaciones()
                                             .FirstOrDefault(e => e.Codigo == codResumen);

                    if (estacionResumen == null)
                    {
                        Console.WriteLine("Estación no encontrada.");
                        break;
                    }

                    estacionResumen.CalcularResumen();
                    break;
                    break;
                case 4:
                    red.ResumenGlobal();
                    break;
                case 5:
                    var estaciones = red.ObtenerEstaciones();
                    if (estaciones.Count == 0)
                    {
                        Console.WriteLine("No hay estaciones registradas.");
                    }
                    else
                    {
                        foreach (var est in estaciones)
                        {
                            est.MostrarInformacion();
                        }
                    }
                    break;
                case 6:
                    volver = true;
                    break;
            }
        }
    }

    static int MostrarMenu(string titulo, string[] opciones)
    {
        Console.WriteLine($"\n===== {titulo} =====");
        for (int i = 0; i < opciones.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {opciones[i]}");
        }
        int opcion;
        do
        {
            Console.Write("Seleccione una opción: ");
        } while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > opciones.Length);

        return opcion;
    }
}

