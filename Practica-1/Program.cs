using RedMeteorologica;
using SistemaParqueadero;
using System;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static RedMeteorologica.RedMeteorologica red = new RedMeteorologica.RedMeteorologica();
    static Parqueadero parqueadero = new Parqueadero();
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
        Console.WriteLine("Sistema finalizado correctamente");
    }

    static void MenuParqueadero()
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

    static void RegistrarEntradaVehiculo()
    {
        Console.Clear();
        Console.WriteLine("=== Registrar Entrada ===");

        string placa;
        bool valido = false;
        do
        {
            Console.Write("Ingrese la placa: ");
            placa = Console.ReadLine();
            if (string.IsNullOrEmpty(placa))
            {
                Console.WriteLine("La placa no puede estar vacía.");
            }
            else if (!Regex.IsMatch(placa, @"^[a-zA-Z0-9]+$"))
            {
                Console.WriteLine($"Placa invalida: (' {placa}'). Solo letras y numeros son permitidos.");
            }
            else if (placa.Length != 6)
            {
                Console.WriteLine($"Placa invalida: (' {placa}'). Debe tener entre 6 caracteres alfanumericos");
            }
            else if (parqueadero.Espacios.FirstOrDefault(e => e.PlacaVehiculo == placa) != null)
            {
                Console.WriteLine("Ya existe un vehiculo con esa placa en el parqueadero actualmente.");
            }
            else
            {
                valido = true;
            }
        }
        while (!valido);

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
        String placa;
        bool valido = false;
        do
        {
            Console.Write("Ingrese la placa: ");
            placa = Console.ReadLine();
            if (string.IsNullOrEmpty(placa))
            {
                Console.WriteLine("La placa no puede estar vacía.");
                RegistrarSalidaYCobro();
            }
            else if (!Regex.IsMatch(placa, @"^[a-zA-Z0-9]+$"))
            {
                Console.WriteLine($"Placa invalida: (' {placa}'). Solo letras y numeros son permitidos.");
                RegistrarSalidaYCobro();
            }
            else if (placa.Length != 6)
            {
                Console.WriteLine($"Placa invalida: (' {placa}'). Debe tener entre 6 caracteres alfanumericos");
                RegistrarSalidaYCobro();
            }
            else if (parqueadero.Espacios.FirstOrDefault(e => e.PlacaVehiculo == placa) == null)
            {
                Console.WriteLine("No esta registrada esa placa en el sistema.");
                RegistrarSalidaYCobro();
            }
            else
            {
                valido = true;
            }
        }
        while (!valido);

        var espacio = parqueadero.Espacios.FirstOrDefault(e => e.EstaOcupado && e.PlacaVehiculo == placa);

        if (espacio != null)
        {
            DateTime horaSalida = DateTime.Now;
            Ticket ticket = espacio.Liberar(horaSalida);

            if (ticket != null)
            {
                parqueadero.Tickets.Add(ticket);

                TimeSpan duracion = ticket.HoraSalida - ticket.HoraEntrada;
                Console.WriteLine($"Duración: {duracion.Hours}h {duracion.Minutes}m");
                Console.WriteLine($"Valor a pagar: {ticket.ValorCobrado:C}");

                Console.Write("Ingrese medio de pago: ");
                string medio = Console.ReadLine();
                parqueadero.ProcesarPago(ticket.ValorCobrado, medio);
                parqueadero.Auditar("Salida", $"Vehículo {placa} salió del espacio {espacio.Id}");
            }
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

