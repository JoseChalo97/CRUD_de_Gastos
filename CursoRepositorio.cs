using CRUD_de_Gastos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CRUD_de_Gastos
{
    public class CursoRepositorio
    {
        private const string CadenaConexion =
            @"Server=(localdb)\MSSQLLocalDB;" +
            @"Database=AcademiaDB;" +
            @"Trusted_Connection=True;" +
            @"TrustServerCertificate=True;";

        public void Insertar(curso g)
        {
            string sql = "INSERT INTO Cursos (Nombre, Instructor, Cuota) " +
                         "VALUES (@Nombre, @Instructor, @Cuota)";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@Nombre", g.Nombre);
                comando.Parameters.AddWithValue("@Instructor", g.Instructor);
                comando.Parameters.AddWithValue("@Cuota", g.Cuota);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public List<curso> ListarTodos()
        {
            var lista = new List<curso>();
            string sql = "SELECT Id, Nombre, Instructor, Cuota FROM Cursos ORDER BY Id";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        lista.Add(new curso
                        {
                            Id = lector.GetInt32(0),
                            Nombre = lector.GetString(1),
                            Cuota = lector.GetDecimal(3),
                            Instructor = lector.GetString(2)
                        });
                    }
                }
            }
            return lista;
        }

        public curso? BuscarPorId(int id)
        {
            string sql = "SELECT Id, Nombre, Instructor, Cuota FROM Cursos WHERE Id = @id";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", id);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return new curso
                        {
                            Id = lector.GetInt32(0),
                            Nombre = lector.GetString(1),
                            Cuota = lector.GetDecimal(3),
                            Instructor = lector.GetString(2)
                        };
                    }
                }
            }
            return null;
        }

        public bool Actualizar(curso g)
        {
            string sql = "UPDATE Cursos SET Nombre = @Nombre, Instructor = @Instructor, Cuota = @Cuota WHERE Id = @id";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@Nombre", g.Nombre);
                comando.Parameters.AddWithValue("@Instructor", g.Instructor);
                comando.Parameters.AddWithValue("@Cuota", g.Cuota);
                comando.Parameters.AddWithValue("@id", g.Id);

                conexion.Open();
                return comando.ExecuteNonQuery() == 1;
            }
        }

        public bool Eliminar(int id)
        {
            string sql = "DELETE FROM Cursos WHERE Id = @id";

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