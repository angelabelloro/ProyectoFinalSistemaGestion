using System.Data;
using System.Data.SqlClient;

namespace proyectoFinalBello
{
    public class UsuarioHandler : DBHandler
    {
        public static bool InicioSesion(string username, string password)
        { 
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using(SqlCommand sqlCommand = new SqlCommand("SELECT Contraseña FROM Usuario WHERE NombreUsuario = @username AND Contraseña = @password", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@username", username);
                    sqlCommand.Parameters.AddWithValue("@password", password);
                    sqlConnection.Open();
                    using(SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while(dataReader.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.NombreUsuario = username;
                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                }
            }
        }
        public Usuario GetUsuariosById(int id)
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "SELECT * FROM Usuario WHERE Id = @idUsuario;";

                    sqlCommand.Parameters.AddWithValue("@idUsuario", id);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = sqlCommand;
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table); //Se ejecuta el Select
                    sqlCommand.Connection.Close();
                    foreach (DataRow row in table.Rows)
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(row["Id"]);
                        usuario.Nombre = row["Nombre"]?.ToString();
                        usuario.Apellido = row["Apellido"]?.ToString();
                        usuario.NombreUsuario = row["NombreUsuario"]?.ToString();
                        usuario.Contraseña = row["Contraseña"]?.ToString();
                        usuario.Mail = row["Mail"]?.ToString();


                        usuarios.Add(usuario);
                    }
                }
            }

            return usuarios?.FirstOrDefault();
        }

        public Usuario GetUsuariosByName(string usuarioName )
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "SELECT * FROM Usuario WHERE NombreUsuario = @usuarioName;";

                    sqlCommand.Parameters.AddWithValue("@usuarioName", usuarioName);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = sqlCommand;
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table); //Se ejecuta el Select
                    sqlCommand.Connection.Close();
                    foreach (DataRow row in table.Rows)
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(row["Id"]);
                        usuario.Nombre = row["Nombre"]?.ToString();
                        usuario.Apellido = row["Apellido"]?.ToString();
                        usuario.NombreUsuario = row["NombreUsuario"]?.ToString();
                        usuario.Contraseña = row["Contraseña"]?.ToString();
                        usuario.Mail = row["Mail"]?.ToString();


                        usuarios.Add(usuario);
                    }
                }
            }

            return usuarios?.FirstOrDefault();
        }
    }
}

