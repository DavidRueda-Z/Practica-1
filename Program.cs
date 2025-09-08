using System;
using SistemaParqueadero;
using RedMeteorologica;

class Program
{
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
                    // TODO: Agregar estación
                    break;
                case 2:
                    // TODO: Registrar lectura
                    break;
                case 3:
                    // TODO: Ver resumen por estación
                    break;
                case 4:
                    // TODO: Resumen global
                    break;
                case 5:
                    // TODO: Listar estaciones
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
