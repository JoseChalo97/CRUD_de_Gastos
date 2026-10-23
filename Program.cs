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
            GastoRepositorio repo = new GastoRepositorio();
            string opcion;

            do
            {
                opcion = LeerOpcionMenu();

                switch (opcion)
                {
                    case "1":
                        RegistrarGasto(repo);
                        break;
                    case "2":
                        ListarGastos(repo);
                        break;
                    case "3":
                        BuscarGastoPorId(repo);
                        break;
                    case "4":
                        ActualizarMontoGasto(repo);
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
            Console.WriteLine("REGISTRO DE GASTOS (CRUD)");
            Console.WriteLine("1. Registrar gasto");
            Console.WriteLine("2. Listar gastos");
            Console.WriteLine("3. Buscar gasto por Id");
            Console.WriteLine("4. Actualizar monto");
            Console.WriteLine("5. Eliminar gasto");
            Console.WriteLine("6. Salir");
            Console.Write("Elige una opción:");
            return Console.ReadLine().Trim();
        }

        private static void RegistrarGasto(GastoRepositorio repo)
        {
            Console.Write("Descripción:");
            string descripcion = Console.ReadLine().Trim();

            Console.Write("Monto:");
            bool montoOk = decimal.TryParse(Console.ReadLine(), out decimal monto);

            Console.Write("Categoría:");
            string categoria = Console.ReadLine().Trim();

            if (!montoOk || monto <= 0 || string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(categoria))
            {
                Console.WriteLine("Datos inválidos.");
                return;
            }

            try
            {
                Gasto nuevoGasto = new Gasto
                {
                    Descripcion = descripcion,
                    Monto = monto,
                    Categoria = categoria
                };
                repo.Insertar(nuevoGasto);
                Console.WriteLine("Gasto registrado.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al registrar: {ex.Message}");
            }
        }

        private static void ListarGastos(GastoRepositorio repo)
        {
            try
            {
                List<Gasto> lista = repo.ListarTodos();

                if (lista.Count == 0)
                {
                    Console.WriteLine("No hay gastos registrados.");
                    return;
                }
                Console.WriteLine($"{"Id",-3} {"Descripcion",-20} Q {"Monto",7}  {"Categoria",-15}");
                decimal totalGastado = 0;
                foreach (Gasto g in lista)
                {
                    Console.WriteLine(g);
                    totalGastado += g.Monto;
                }

                Console.WriteLine($"TOTAL GASTADO: Q {totalGastado:N2}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al listar: {ex.Message}");
            }
        }

        private static void BuscarGastoPorId(GastoRepositorio repo)
        {
            Console.Write("Id del gasto a buscar:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            try
            {
                Gasto? gasto = repo.BuscarPorId(id);
                Console.WriteLine(gasto == null ? "No existe ese Id." : gasto.ToString());
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al buscar: {ex.Message}");
            }
        }

        private static void ActualizarMontoGasto(GastoRepositorio repo)
        {
            Console.Write("Id del gasto a actualizar:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            try
            {
                Gasto? gasto = repo.BuscarPorId(id);
                if (gasto == null)
                {
                    Console.WriteLine("No existe ese Id.");
                    return;
                }

                Console.WriteLine($"Gasto actual: {gasto}");
                Console.Write("Nuevo monto:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal nuevoMonto) || nuevoMonto <= 0)
                {
                    Console.WriteLine("Monto inválido.");
                    return;
                }

                gasto.Monto = nuevoMonto;
                bool actualizado = repo.Actualizar(gasto);
                Console.WriteLine(actualizado ? "Gasto actualizado." : "No se actualizó.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al actualizar: {ex.Message}");
            }
        }

        private static void EliminarGasto(GastoRepositorio repo)
        {
            Console.Write("Id del gasto a eliminar:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            try
            {
                bool eliminado = repo.Eliminar(id);
                Console.WriteLine(eliminado ? "Gasto eliminado." : "No existe ese Id.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al eliminar: {ex.Message}");
            }
        }
    }
}