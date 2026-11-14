using CRUD_de_Gastos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CRUD_de_Gastos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CursoRepositorio repo = new CursoRepositorio();
            string opcion;

            do
            {
                opcion = LeerOpcionMenu();

                switch (opcion)
                {
                    case "1":
                        RegistrarCurso(repo);
                        break;
                    case "2":
                        ListarCurso(repo);
                        break;
                    case "3":
                        BuscarCursoPorId(repo);
                        break;
                    case "4":
                        ActualizarCuotaCurso(repo);
                        break;
                    case "5":
                        EliminarGasto(repo);
                        break;
                    case "6":
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                Console.WriteLine();
            }
            while (opcion != "6");
        }

        private static string LeerOpcionMenu()
        {
            Console.WriteLine("REGISTRO DE Cursos (CRUD)");
            Console.WriteLine("1. Registrar Curso");
            Console.WriteLine("2. Listar Curso");
            Console.WriteLine("3. Buscar Curso por Id");
            Console.WriteLine("4. Actualizar Cuota");
            Console.WriteLine("5. Eliminar Curso");
            Console.WriteLine("6. Salir");
            Console.Write("Elige una opción:");
            return Console.ReadLine().Trim();
        }

        private static void RegistrarCurso(CursoRepositorio repo)
        {
            Console.Write("Nombre:");
            string Nombre = Console.ReadLine().Trim();

            Console.Write("Instructor:");
            string Instructor = Console.ReadLine().Trim();

            Console.Write("Cuota:");
            bool CuotaOk = decimal.TryParse(Console.ReadLine(), out decimal Cuota);

            if (!CuotaOk || Cuota <= 0 || string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Instructor))
            {
                Console.WriteLine("Datos inválidos.");
                return;
            }

            try
            {
                curso nuevoCurso = new curso 
                {
                    Nombre = Nombre,
                    Cuota = Cuota,
                    Instructor = Instructor
                };
                repo.Insertar(nuevoCurso);
                Console.WriteLine("Curso registrado.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al registrar: {ex.Message}");
            }
        }

        private static void ListarCurso(CursoRepositorio repo)
        {
            try
            {
                List<curso> lista = repo.ListarTodos();

                if (lista.Count == 0)
                {
                    Console.WriteLine("No hay Cursos registrados.");
                    return;
                }
                Console.WriteLine($"{"Id",-3} {"Nombre",-20} {"Instructor",-15} Q {"Cuota",7}");
                decimal totalCuotas = 0;
                foreach (curso g in lista)
                {
                    Console.WriteLine(g);
                    totalCuotas += g.Cuota;
                }

                Console.WriteLine($"TOTAL CUOTAS: Q {totalCuotas:N2}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al listar: {ex.Message}");
            }
        }

        private static void BuscarCursoPorId(CursoRepositorio repo)
        {
            Console.Write("Id del Curso a buscar:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            try
            {
                curso? curso = repo.BuscarPorId(id);
                Console.WriteLine(curso == null ? "No existe ese Id." : curso.ToString());
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al buscar: {ex.Message}");
            }
        }

        private static void ActualizarCuotaCurso(CursoRepositorio repo)
        {
            Console.Write("Id del Curso a actualizar:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            try
            {
                curso? Curso = repo.BuscarPorId(id);
                if (Curso == null)
                {
                    Console.WriteLine("No existe ese Id.");
                    return;
                }

                Console.WriteLine($"Gasto actual: {Curso}");
                Console.Write("Nueva Cuota:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal nuevoCuota) || nuevoCuota <= 0)
                {
                    Console.WriteLine("Cuota inválido.");
                    return;
                }

                Curso.Cuota = nuevoCuota;
                bool actualizado = repo.Actualizar(Curso);
                Console.WriteLine(actualizado ? "Curso actualizado." : "No se actualizó.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al actualizar: {ex.Message}");
            }
        }

        private static void EliminarGasto(CursoRepositorio repo)
        {
            Console.Write("Id del Curso a eliminar:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            try
            {
                bool eliminado = repo.Eliminar(id);
                Console.WriteLine(eliminado ? "Curso eliminado." : "No existe ese Id.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al eliminar: {ex.Message}");
            }
        }
    }
}