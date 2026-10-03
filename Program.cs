using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string archivo = "vuelos.txt";

        if (!File.Exists(archivo))
        {
            Console.WriteLine("No se encontró el archivo vuelos.txt");
            return;
        }

        List<Vuelo> vuelos = new List<Vuelo>();

        string[] lineas = File.ReadAllLines(archivo);

        foreach (string linea in lineas)
        {
            if (string.IsNullOrWhiteSpace(linea))
            {
                continue;
            }

            string[] datos = linea.Split('|');

            if (datos.Length != 4)
            {
                continue;
            }

            if (!decimal.TryParse(datos[3], out decimal precio))
            {
                continue;
            }

            Vuelo vuelo = new Vuelo(
                datos[0],
                datos[1],
                datos[2],
                precio
            );

            vuelos.Add(vuelo);
        }

        Console.WriteLine("========================================");
        Console.WriteLine("       SISTEMA DE VUELOS BARATOS");
        Console.WriteLine("========================================");

        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine();
            Console.WriteLine("1. Mostrar todos los vuelos");
            Console.WriteLine("2. Buscar vuelos por origen y destino");
            Console.WriteLine("3. Mostrar el vuelo más barato");
            Console.WriteLine("4. Buscar por aerolínea");
            Console.WriteLine("5. Mostrar reporte");
            Console.WriteLine("6. Salir");
            Console.WriteLine();

            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    MostrarTodosLosVuelos(vuelos);
                    break;

                case "2":
                    BuscarPorOrigenDestino(vuelos);
                    break;

                case "3":
                    MostrarVueloMasBarato(vuelos);
                    break;

                case "4":
                    BuscarPorAerolinea(vuelos);
                    break;

                case "5":
                    MostrarReporte(vuelos);
                    break;

                case "6":
                    Console.WriteLine("Gracias por utilizar el sistema.");
                    continuar = false;
                    break;

                default:
                    Console.WriteLine("Opción no válida. Seleccione una opción del 1 al 6.");
                    break;
            }

            if (continuar)
            {
                Console.WriteLine();
                Console.WriteLine("Presione ENTER para continuar...");
                Console.ReadLine();
            }
        }
    }

    static void MostrarTodosLosVuelos(List<Vuelo> vuelos)
    {
        Console.WriteLine("========== TODOS LOS VUELOS ==========");

        foreach (Vuelo vuelo in vuelos)
        {
            MostrarVuelo(vuelo);
        }
    }

    static void BuscarPorOrigenDestino(List<Vuelo> vuelos)
    {
        Console.WriteLine("===== BUSCAR POR ORIGEN Y DESTINO =====");

        Console.Write("Ingrese el origen: ");
        string origen = Console.ReadLine();

        Console.Write("Ingrese el destino: ");
        string destino = Console.ReadLine();

        bool encontrado = false;

        foreach (Vuelo vuelo in vuelos)
        {
            if (vuelo.Origen.Equals(origen, StringComparison.OrdinalIgnoreCase) &&
                vuelo.Destino.Equals(destino, StringComparison.OrdinalIgnoreCase))
            {
                MostrarVuelo(vuelo);
                encontrado = true;
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("No se encontraron vuelos para esa ruta.");
        }
    }

    static void MostrarVueloMasBarato(List<Vuelo> vuelos)
    {
        Console.WriteLine("========== VUELO MÁS BARATO ==========");

        if (vuelos.Count == 0)
        {
            Console.WriteLine("No existen vuelos disponibles.");
            return;
        }

        Vuelo masBarato = vuelos[0];

        foreach (Vuelo vuelo in vuelos)
        {
            if (vuelo.Precio < masBarato.Precio)
            {
                masBarato = vuelo;
            }
        }

        MostrarVuelo(masBarato);
    }

    static void BuscarPorAerolinea(List<Vuelo> vuelos)
    {
        Console.WriteLine("========== BUSCAR POR AEROLÍNEA ==========");

        Console.Write("Ingrese la aerolínea: ");
        string aerolinea = Console.ReadLine();

        bool encontrado = false;

        foreach (Vuelo vuelo in vuelos)
        {
            if (vuelo.Aerolinea.Equals(aerolinea, StringComparison.OrdinalIgnoreCase))
            {
                MostrarVuelo(vuelo);
                encontrado = true;
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("No se encontraron vuelos de esa aerolínea.");
        }
    }

    static void MostrarReporte(List<Vuelo> vuelos)
    {
        Console.WriteLine("=============== REPORTE ===============");

        if (vuelos.Count == 0)
        {
            Console.WriteLine("No existen vuelos registrados.");
            return;
        }

        decimal precioTotal = 0;
        Vuelo masBarato = vuelos[0];
        Vuelo masCaro = vuelos[0];

        foreach (Vuelo vuelo in vuelos)
        {
            precioTotal += vuelo.Precio;

            if (vuelo.Precio < masBarato.Precio)
            {
                masBarato = vuelo;
            }

            if (vuelo.Precio > masCaro.Precio)
            {
                masCaro = vuelo;
            }
        }

        decimal promedio = precioTotal / vuelos.Count;

        Console.WriteLine("Total de vuelos: " + vuelos.Count);
        Console.WriteLine("Precio más bajo: $" + masBarato.Precio);
        Console.WriteLine("Precio más alto: $" + masCaro.Precio);
        Console.WriteLine("Precio promedio: $" + promedio.ToString("0.00"));

        Console.WriteLine();
        Console.WriteLine("Vuelo más barato:");
        MostrarVuelo(masBarato);

        Console.WriteLine();
        Console.WriteLine("Vuelo más caro:");
        MostrarVuelo(masCaro);
    }

    static void MostrarVuelo(Vuelo vuelo)
    {
        Console.WriteLine(
            vuelo.Origen + " -> " +
            vuelo.Destino +
            " | " +
            vuelo.Aerolinea +
            " | $" +
            vuelo.Precio.ToString("0.00")
        );
    }
}
