namespace proyectoFinalBello
{
    //Modelo
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Mail { get; set; }

    }

    public class Producto
    {
        public int Id { get; set; }
        public string Descripciones { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }
    }

    class ProductoVendido : Producto
    {
        public int Id { get; set; }
        public long IdVenta { get; set; }
    }
    public class Venta
    {
        private int Id { get; set; }
        private string Comentarios { get; set; }
    }
}
