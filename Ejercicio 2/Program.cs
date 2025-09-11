//using SistemaParqueadero;
using RedMeteorologica;
using System;
using System.Text.RegularExpressions;

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
                    red.AgregarEstacion();
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
                    String codresumen;
                    bool valido = false;
                    do
                    {
                        Console.Write("Código de la estación: ");
                        codresumen = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(codresumen) || codresumen.Length < 3 || codresumen.Length > 10)
                        {
                            Console.WriteLine($"Código inválido (' {codresumen}'). Debe tener entre 3 y 10 caracteres alfanumericos.");
                        }
                        else if (!Regex.IsMatch(codresumen, @"^[a-zA-Z0-9]+$"))
                        {
                            Console.WriteLine($"Código inválido (' {codresumen}'). Solo letras y numeros son permitidos.");
                        }
                        else if (string.IsNullOrEmpty(codresumen))
                        {
                            Console.WriteLine("El código no puede estar vacío.");
                        }
                        else
                        {
                            valido = true;
                        }
                    }
                    while (!valido);

                    var estacionResumen = red.ObtenerEstaciones()
                                             .FirstOrDefault(e => e.Codigo == codresumen);

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
                    Console.WriteLine("\nLista de Estaciones");
                    foreach (var est in estaciones)
                    {
                        est.MostrarInformacion();
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

