using System;

class Vehiculo
{
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Anio { get; set; }
    public decimal Precio { get; set; }
    public Vehiculo? Siguiente { get; set; } = null;
}

class ListaVehiculos
{
    private Vehiculo? cabeza;

    public void AgregarVehiculo(string placa, string marca, string modelo, int anio, decimal precio)
    {
        Vehiculo nuevo = new Vehiculo
        {
            Placa = placa,
            Marca = marca,
            Modelo = modelo,
            Anio = anio,
            Precio = precio,
            Siguiente = null
        };

        if (cabeza == null)
        {
            cabeza = nuevo;
        }
        else
        {
            Vehiculo actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo;
        }
        Console.WriteLine("Vehículo agregado correctamente.");
    }

    public void BuscarPorPlaca(string placa)
    {
        Vehiculo? actual = cabeza;
        while (actual != null)
        {
            if (actual.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Placa: {actual.Placa}, Marca: {actual.Marca}, Modelo: {actual.Modelo}, Año: {actual.Anio}, Precio: {actual.Precio:C}");
                return;
            }
            actual = actual.Siguiente;
        }
        Console.WriteLine("Vehículo no encontrado.");
    }

    public void VerPorAnio(int anio)
    {
        Vehiculo? actual = cabeza;
        bool encontrado = false;
        while (actual != null)
        {
            if (actual.Anio == anio)
            {
                Console.WriteLine($"Placa: {actual.Placa}, Marca: {actual.Marca}, Modelo: {actual.Modelo}, Precio: {actual.Precio:C}");
                encontrado = true;
            }
            actual = actual.Siguiente;
        }
        if (!encontrado)
            Console.WriteLine("No hay vehículos registrados de ese año.");
    }

    public void VerTodos()
    {
        Vehiculo? actual = cabeza;
        if (actual == null)
        {
            Console.WriteLine("No hay vehículos registrados.");
            return;
        }
        while (actual != null)
        {
            Console.WriteLine($"Placa: {actual.Placa}, Marca: {actual.Marca}, Modelo: {actual.Modelo}, Año: {actual.Anio}, Precio: {actual.Precio:C}");
            actual = actual.Siguiente;
        }
    }

    public void EliminarVehiculo(string placa)
    {
        if (cabeza == null)
        {
            Console.WriteLine("No hay vehículos para eliminar.");
            return;
        }

        if (cabeza.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase))
        {
            cabeza = cabeza.Siguiente;
            Console.WriteLine("Vehículo eliminado.");
            return;
        }

        Vehiculo? actual = cabeza;
        Vehiculo? anterior = null;

        while (actual != null && !actual.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase))
        {
            anterior = actual;
            actual = actual.Siguiente;
        }

        if (actual == null)
        {
            Console.WriteLine("Vehículo no encontrado.");
            return;
        }

        if (anterior != null)
        {
            anterior.Siguiente = actual.Siguiente;
            Console.WriteLine("Vehículo eliminado.");
        }
    }
}

class Program
{
    static void Main()
    {
        ListaVehiculos lista = new ListaVehiculos();
        while (true)
        {
            Console.WriteLine("\n--- Menú Vehículos ---");
            Console.WriteLine("1. Agregar vehículo");
            Console.WriteLine("2. Buscar vehículo por placa");
            Console.WriteLine("3. Ver vehículos por año");
            Console.WriteLine("4. Ver todos los vehículos");
            Console.WriteLine("5. Eliminar vehículo");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese placa: ");
                    string? placa = Console.ReadLine();
                    Console.Write("Ingrese marca: ");
                    string? marca = Console.ReadLine();
                    Console.Write("Ingrese modelo: ");
                    string? modelo = Console.ReadLine();

                    Console.Write("Ingrese año: ");
                    string? anioStr = Console.ReadLine();
                    if (!int.TryParse(anioStr, out int anio))
                    {
                        Console.WriteLine("Año inválido.");
                        break;
                    }

                    Console.Write("Ingrese precio: ");
                    string? precioStr = Console.ReadLine();
                    if (!decimal.TryParse(precioStr, out decimal precio))
                    {
                        Console.WriteLine("Precio inválido.");
                        break;
                    }

                    if (string.IsNullOrWhiteSpace(placa) || string.IsNullOrWhiteSpace(marca) || string.IsNullOrWhiteSpace(modelo))
                    {
                        Console.WriteLine("Los campos placa, marca y modelo no pueden estar vacíos.");
                        break;
                    }

                    lista.AgregarVehiculo(placa, marca, modelo, anio, precio);
                    break;

                case "2":
                    Console.Write("Ingrese placa a buscar: ");
                    string? placaBuscar = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(placaBuscar))
                        lista.BuscarPorPlaca(placaBuscar);
                    else
                        Console.WriteLine("La placa no puede estar vacía.");
                    break;

                case "3":
                    Console.Write("Ingrese año a filtrar: ");
                    string? anioFiltroStr = Console.ReadLine();
                    if (int.TryParse(anioFiltroStr, out int anioFiltro))
                        lista.VerPorAnio(anioFiltro);
                    else
                        Console.WriteLine("Año inválido.");
                    break;

                case "4":
                    lista.VerTodos();
                    break;

                case "5":
                    Console.Write("Ingrese placa a eliminar: ");
                    string? placaEliminar = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(placaEliminar))
                        lista.EliminarVehiculo(placaEliminar);
                    else
                        Console.WriteLine("La placa no puede estar vacía.");
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }
}
