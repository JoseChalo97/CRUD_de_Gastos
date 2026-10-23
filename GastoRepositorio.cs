using CRUD_de_Gastos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CRUD_de_Gastos
{
    public class GastoRepositorio
    {
        private const string CadenaConexion =
            @"Server=(localdb)\MSSQLLocalDB;" +
            @"Database=GastosDB;" +
            @"Trusted_Connection=True;" +
            @"TrustServerCertificate=True;";

        public void Insertar(Gasto g)
        {
            string sql = "INSERT INTO Gastos (Descripcion, Monto, Categoria) " +
                         "VALUES (@descripcion, @monto, @categoria)";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@descripcion", g.Descripcion);
                comando.Parameters.AddWithValue("@monto", g.Monto);
                comando.Parameters.AddWithValue("@categoria", g.Categoria);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public List<Gasto> ListarTodos()
        {
            var lista = new List<Gasto>();
            string sql = "SELECT Id, Descripcion, Monto, Categoria FROM Gastos ORDER BY Id";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        lista.Add(new Gasto
                        {
                            Id = lector.GetInt32(0),
                            Descripcion = lector.GetString(1),
                            Monto = lector.GetDecimal(2),
                            Categoria = lector.GetString(3)
                        });
                    }
                }
            }
            return lista;
        }

        public Gasto? BuscarPorId(int id)
        {
            string sql = "SELECT Id, Descripcion, Monto, Categoria FROM Gastos WHERE Id = @id";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", id);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return new Gasto
                        {
                            Id = lector.GetInt32(0),
                            Descripcion = lector.GetString(1),
                            Monto = lector.GetDecimal(2),
                            Categoria = lector.GetString(3)
                        };
                    }
                }
            }
            return null; 
        }

        public bool Actualizar(Gasto g)
        {
            string sql = "UPDATE Gastos SET Descripcion = @descripcion, Monto = @monto, Categoria = @categoria WHERE Id = @id";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@descripcion", g.Descripcion);
                comando.Parameters.AddWithValue("@monto", g.Monto);
                comando.Parameters.AddWithValue("@categoria", g.Categoria);
                comando.Parameters.AddWithValue("@id", g.Id);

                conexion.Open();
                return comando.ExecuteNonQuery() == 1; 
            }
        }

        public bool Eliminar(int id)
        {
            string sql = "DELETE FROM Gastos WHERE Id = @id";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", id);
                conexion.Open();
                return comando.ExecuteNonQuery() == 1; 
            }
        }
    }
}