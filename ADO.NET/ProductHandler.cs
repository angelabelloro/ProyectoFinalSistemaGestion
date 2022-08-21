using System.Data;
using System.Data.SqlClient;

namespace proyectoFinalBello
{
    public partial class ProductHandler : DBHandler
    {
        public List<Producto> GetProductos()
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * FROM Producto", sqlConnection))
                {
                    //abro conexión 
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        //Compruebo si hay datos en las filas para leer
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(dataReader["ID"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();
                                productos.Add(producto);
                            }
                        }
                    }
                    sqlConnection.Close();
                }

            }
            return productos;
        }
        public void GetProductsByParameterUserId(Usuario usuario)
        {
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(ConnectionString))
                {
                    string queryProductosUserId = "SELECT * FROM Producto WHERE IdUsuario = @idUsuario;";
                    SqlParameter parametro = new SqlParameter();
                    parametro.ParameterName = "idUsuario";
                    parametro.SqlDbType = System.Data.SqlDbType.BigInt;
                    parametro.Value = usuario.Id;

                    sqlconnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryProductosUserId, sqlconnection))
                    {
                        sqlCommand.Parameters.Add(parametro);
                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlconnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public List<Producto> GetProductosByIdUsuario(int userID)
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "SELECT * FROM Producto WHERE IdUsuario = @idUsuario;";

                    sqlCommand.Parameters.AddWithValue("@idUsuario", userID);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = sqlCommand;
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table); //Se ejecuta el Select
                    sqlCommand.Connection.Close();
                    foreach (DataRow row in table.Rows)
                    {
                        Producto producto = new Producto();
                        producto.Id = Convert.ToInt32(row["ID"]);
                        producto.Stock = Convert.ToInt32(row["Stock"]);
                        producto.IdUsuario = Convert.ToInt32(row["IdUsuario"]);
                        producto.Costo = Convert.ToInt32(row["Costo"]);
                        producto.PrecioVenta = Convert.ToInt32(row["PrecioVenta"]);
                        producto.Descripciones = row["Descripciones"].ToString();
                        productos.Add(producto);
                    }
                }
            }
            return productos;
        }
        

    }
}
