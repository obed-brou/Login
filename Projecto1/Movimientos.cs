using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto1
{
    class Movimientos
    {
        public class DatabaseManager
        {
            private string connectionString;


            public DatabaseManager(string connectionString)
            {
                this.connectionString = connectionString;
            }

            public void ConnectAndQuery()
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);

                try
                {
                    connection.Open();

                    // Realizar consultas aquí
                    string query = "SELECT * FROM Usuarios";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    using MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Procesar resultados
                        string nombreCompleto = reader.GetString("NombreCompleto");
                        // Otros campos
                        Console.WriteLine(nombreCompleto);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
                }
            }
            public bool CrearUsuario(string nombreCompleto, string telefono, string email, string genero, DateTime fechaNacimiento, string contraseña)
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);

                try
                {
                    connection.Open();

                    string query = @"INSERT INTO Usuarios (NombreCompleto, Telefono, Email, Genero, FechaNacimiento, Contraseña) 
                                 VALUES (@NombreCompleto, @Telefono, @Email, @Genero, @FechaNacimiento, @Contraseña)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Genero", genero);
                    command.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                    command.Parameters.AddWithValue("@Contraseña", contraseña);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true; // Usuario creado exitosamente
                    }
                    else
                    {
                        return false; // Fallo al crear usuario
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al crear usuario: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
