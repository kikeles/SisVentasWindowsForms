namespace Sistema.Entidades
{
    public class Categoria
    {
        //las propiedades reflejan los campos de dicha tabla
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
