using System.Data;
using System.Data.SqlClient;

namespace proyectoFinalBello
{
    public partial class ProductHandler
    {
        public class ProductosVendidosHandler : ProductHandler
        {
            public void GetSoldProductsByParameterUserId(Usuario usuario)
            {
                try
                {
                    using (SqlConnection sqlconnection = new SqlConnection(ConnectionString))
                    {
                        string querySoldProductosUserId = "SELECT * FROM ProductoVendido pv INNER JOIN Producto p ON p.Id = pv.IdProducto INNER JOIN Usuario u ON u.Id = p.IdUsuario WHERE p.IdUsuario = @idUsuario";
                        SqlParameter parametro = new SqlParameter();
                        parametro.ParameterName = "idUsuario";
                        parametro.SqlDbType = System.Data.SqlDbType.BigInt;
                        parametro.Value = usuario.Id;

                        sqlconnection.Open();

                        using (SqlCommand sqlCommand = new SqlCommand(querySoldProductosUserId, sqlconnection))
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
            public List<Producto> GetProductosVendidos(int userID)
            {
                List<Producto> productosVendidos = new List<Producto>();
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.Connection.Open();
                        sqlCommand.CommandText = "SELECT * FROM ProductoVendido pv INNER JOIN Producto p ON p.Id = pv.IdProducto INNER JOIN Usuario u ON u.Id = p.IdUsuario" +
                            "";

                        sqlCommand.Parameters.AddWithValue("@id", userID);

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
                            productosVendidos.Add(producto);
                        }
                    }
                }
                return productosVendidos;
            }

        }

    }
}
