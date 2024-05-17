using Microsoft.Maui.ApplicationModel.Communication;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Projecto1
{
    public class Conexion  
    {
        string connectionString;
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

        //public Conexion(string server, string database, string username, string password)

        //{

        //    connectionString = $"server={server};port=3306;database={database};uid={username};pwd={password};SSL Mode = none;";
        //    builder.Server = server;
        //    builder.UserID = username;
        //    builder.Password = password;
        //    builder.Database = database;
        //    builder.SslMode = MySqlSslMode.None;
        //}



        public Conexion()
        {
            // Aquí puedes establecer la cadena de conexión
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "192.168.137.48",
                Port = 3306,
                Database = "AppMovil",
                UserID = "botAppMovil",
                Password = "MANchas231-",
                SslMode = MySqlSslMode.None
            };

            connectionString = builder.ConnectionString;
        }

        public bool CrearUsuario(string nombreCompleto, string telefono, string email, string genero, DateTime fechaNacimiento, string contraseña)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                // Verificar si el usuario ya existe en la base de datos
                string verificarQuery = "SELECT COUNT(*) FROM Usuarios WHERE Email = @Email";
                MySqlCommand verificarCommand = new MySqlCommand(verificarQuery, connection);
                verificarCommand.Parameters.AddWithValue("@Email", email);
                int count = Convert.ToInt32(verificarCommand.ExecuteScalar());

                if (count > 0)
                {
                    Console.WriteLine("El usuario ya existe en la base de datos.");
                    return false;
                }

                // Si el usuario no existe, proceder con la inserción
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

                return rowsAffected > 0;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al crear usuario: " + ex.Message);
                return false;
            }
        }

        public bool EsInicioSesionValido(string usuario, string contraseña)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Email = @Usuario AND Contraseña = @Contraseña";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Usuario", usuario);
                    command.Parameters.AddWithValue("@Contraseña", contraseña);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al iniciar sesión: " + ex.Message);
                // Agrega la excepción al registro o a un archivo de registro para un análisis posterior
                // logger.LogError("Error al iniciar sesión: " + ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado al iniciar sesión: " + ex.Message);
                // Agrega la excepción al registro o a un archivo de registro para un análisis posterior
                // logger.LogError("Error inesperado al iniciar sesión: " + ex.ToString());
                return false;
            }
        }
        public bool CambiarContrasena(string usuario, string nuevaContrasena)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Verificar si la nueva contraseña ya existe en la base de datos
                    string verificarQuery = "SELECT COUNT(*) FROM Usuarios WHERE Contraseña = @NuevaContrasena";
                    MySqlCommand verificarCommand = new MySqlCommand(verificarQuery, connection);
                    verificarCommand.Parameters.AddWithValue("@NuevaContrasena", nuevaContrasena);
                    int count = Convert.ToInt32(verificarCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        Console.WriteLine("La nueva contraseña ya existe en la base de datos. No se puede realizar el cambio.");
                        return false;
                    }

                    // Si la nueva contraseña no existe, proceder con la actualización
                    string query = "UPDATE Usuarios SET Contraseña = @NuevaContrasena WHERE Email = @Usuario";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NuevaContrasena", nuevaContrasena);
                    command.Parameters.AddWithValue("@Usuario", usuario);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al cambiar contraseña: " + ex.Message);
                // Agrega la excepción al registro o a un archivo de registro para un análisis posterior
                // logger.LogError("Error al cambiar contraseña: " + ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado al cambiar contraseña: " + ex.Message);
                // Agrega la excepción al registro o a un archivo de registro para un análisis posterior
                // logger.LogError("Error inesperado al cambiar contraseña: " + ex.ToString());
                return false;
            }
        }




    }
}
