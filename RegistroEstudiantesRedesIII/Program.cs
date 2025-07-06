using System;

class Estudiante
{
    public string Cedula { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public double NotaDefinitiva { get; set; }
    public Estudiante? Siguiente { get; set; }
}

class ListaEstudiantes
{
    private Estudiante? cabeza = null;

    public void AgregarEstudiante(string cedula, string nombre, string apellido, string correo, double nota)
    {
        Estudiante nuevo = new Estudiante()
        {
            Cedula = cedula,
            Nombre = nombre,
            Apellido = apellido,
            Correo = correo,
            NotaDefinitiva = nota
        };

        if (nota >= 7)
        {
            // Insertar por inicio (aprobados)
            nuevo.Siguiente = cabeza;
            cabeza = nuevo;
        }
        else
        {
            // Insertar por final (reprobados)
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Estudiante actual = cabeza;
                while (actual.Siguiente != null)
                    actual = actual.Siguiente;
                actual.Siguiente = nuevo;
            }
        }
    }

    public Estudiante? BuscarPorCedula(string cedula)
    {
        Estudiante? actual = cabeza;
        while (actual != null)
        {
            if (actual.Cedula == cedula)
                return actual;
            actual = actual.Siguiente;
        }
        return null;
    }

    public bool EliminarEstudiante(string cedula)
    {
        if (cabeza == null)
            return false;

        if (cabeza.Cedula == cedula)
        {
            cabeza = cabeza.Siguiente;
            return true;
        }

        Estudiante? actual = cabeza;
        while (actual.Siguiente != null)
        {
            if (actual.Siguiente.Cedula == cedula)
            {
                actual.Siguiente = actual.Siguiente.Siguiente;
                return true;
            }
            actual = actual.Siguiente;
        }
        return false;
    }

    public int TotalAprobados()
    {
        int count = 0;
        Estudiante? actual = cabeza;
        while (actual != null)
        {
            if (actual.NotaDefinitiva >= 7)
                count++;
            actual = actual.Siguiente;
        }
        return count;
    }

    public int TotalReprobados()
    {
        int count = 0;
        Estudiante? actual = cabeza;
        while (actual != null)
        {
            if (actual.NotaDefinitiva < 7)
                count++;
            actual = actual.Siguiente;
        }
        return count;
    }

    public void MostrarTodos()
    {
        Estudiante? actual = cabeza;
        if (actual == null)
        {
            Console.WriteLine("No hay estudiantes registrados.");
            return;
        }
        Console.WriteLine("Lista de estudiantes:");
        while (actual != null)
        {
            Console.WriteLine($"Cédula: {actual.Cedula}, Nombre: {actual.Nombre} {actual.Apellido}, Correo: {actual.Correo}, Nota: {actual.NotaDefinitiva}");
            actual = actual.Siguiente;
        }
    }
}

class Program
{
    static void Main()
    {
        ListaEstudiantes lista = new ListaEstudiantes();
        string? opcion;

        do
        {
            Console.WriteLine("\n--- Menú ---");
            Console.WriteLine("1. Agregar estudiante");
            Console.WriteLine("2. Buscar estudiante por cédula");
            Console.WriteLine("3. Eliminar estudiante");
            Console.WriteLine("4. Mostrar total aprobados");
            Console.WriteLine("5. Mostrar total reprobados");
            Console.WriteLine("6. Mostrar todos los estudiantes");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Cédula: ");
                    string? cedula = Console.ReadLine();
                    Console.Write("Nombre: ");
                    string? nombre = Console.ReadLine();
                    Console.Write("Apellido: ");
                    string? apellido = Console.ReadLine();
                    Console.Write("Correo: ");
                    string? correo = Console.ReadLine();
                    Console.Write("Nota definitiva (1-10): ");
                    string? notaStr = Console.ReadLine();

                    if (cedula == null || nombre == null || apellido == null || correo == null || notaStr == null)
                    {
                        Console.WriteLine("Datos inválidos.");
                        break;
                    }

                    if (!double.TryParse(notaStr, out double nota) || nota < 1 || nota > 10)
                    {
                        Console.WriteLine("Nota inválida. Debe ser un número entre 1 y 10.");
                        break;
                    }

                    lista.AgregarEstudiante(cedula, nombre, apellido, correo, nota);
                    Console.WriteLine("Estudiante agregado.");
                    break;

                case "2":
                    Console.Write("Ingrese cédula a buscar: ");
                    string? cedulaBuscar = Console.ReadLine();
                    if (cedulaBuscar == null)
                    {
                        Console.WriteLine("Cédula inválida.");
                        break;
                    }
                    Estudiante? encontrado = lista.BuscarPorCedula(cedulaBuscar);
                    if (encontrado != null)
                    {
                        Console.WriteLine($"Encontrado: {encontrado.Cedula}, {encontrado.Nombre} {encontrado.Apellido}, Correo: {encontrado.Correo}, Nota: {encontrado.NotaDefinitiva}");
                    }
                    else
                    {
                        Console.WriteLine("Estudiante no encontrado.");
                    }
                    break;

                case "3":
                    Console.Write("Ingrese cédula a eliminar: ");
                    string? cedulaEliminar = Console.ReadLine();
                    if (cedulaEliminar == null)
                    {
                        Console.WriteLine("Cédula inválida.");
                        break;
                    }
                    bool eliminado = lista.EliminarEstudiante(cedulaEliminar);
                    Console.WriteLine(eliminado ? "Estudiante eliminado." : "Estudiante no encontrado.");
                    break;

                case "4":
                    Console.WriteLine($"Total estudiantes aprobados: {lista.TotalAprobados()}");
                    break;

                case "5":
                    Console.WriteLine($"Total estudiantes reprobados: {lista.TotalReprobados()}");
                    break;

                case "6":
                    lista.MostrarTodos();
                    break;

                case "0":
                    Console.WriteLine("Saliendo...");
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

        } while (opcion != "0");
    }
}
