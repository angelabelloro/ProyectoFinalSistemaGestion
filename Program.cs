namespace proyectoFinalBello
{
    public class ProbarObjetos
    {
        static void Main(string[] args)
        {
            ProductHandler productHandler = new ProductHandler();
            productHandler.GetProductos();

            UsuarioHandler usuarioHandler = new UsuarioHandler();
            usuarioHandler.GetUsuariosByName("eperez");

            VentaHandler ventaHandler = new VentaHandler();
            //ventaHandler.GetVentas();
        }
    }
}
