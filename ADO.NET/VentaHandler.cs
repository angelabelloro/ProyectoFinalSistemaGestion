﻿using System.Data;
using System.Data.SqlClient;

namespace proyectoFinalBello
{
    public class VentaHandler:DBHandler
    {

        public void GetVentasbyUserId(Usuario usuario)
        {
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(ConnectionString))
                {
                    string queryProductosUserId = "SELECT * FROM Venta v INNER JOIN ProductoVendido pv ON pv.IdVenta = v.Id INNER JOIN Producto p ON p.Id = pv.IdProducto WHERE p.IdUsuario = @idUsuario";
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
    }
}
