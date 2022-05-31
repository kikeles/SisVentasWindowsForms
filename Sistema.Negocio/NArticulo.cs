using System;
using System.Data;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema.Negocio
{
    public class NArticulo
    {
        public static DataTable Listar()
        {
            DArticulo Datos = new DArticulo();
            return Datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Buscar(valor);
        }

        public static DataTable BuscarVenta(string valor)
        {
            DArticulo Datos = new DArticulo();
            return Datos.BuscarVenta(valor);
        }

        public static DataTable BuscarCodigo(string valor)
        {
            DArticulo Datos = new DArticulo();
            return Datos.BuscarCodigo(valor);
        }

        public static DataTable BuscarCodigoVenta(string valor)
        {
            DArticulo Datos = new DArticulo();
            return Datos.BuscarCodigoVenta(valor);
        }

        public static string Insertar(int IdCategoria, string Codigo, string Nombre, 
            decimal PrecioVenta, int Stock, string Descripcion, string Imagen)
        {
            DArticulo Datos = new DArticulo();
            //validar si ya existe el articulo
            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals("1"))
            {
                return "La artículo ya existe";
            }
            else
            {
                //si pasa la validacion se crea la categoria
                Articulo Obj = new Articulo();
                Obj.IdCategoria = IdCategoria;
                Obj.Codigo = Codigo;
                Obj.Nombre = Nombre;
                Obj.PrecioVenta = PrecioVenta;
                Obj.Stock = Stock;
                Obj.Descripcion = Descripcion;
                Obj.Imagen = Imagen;
                return Datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int Id, int IdCategoria, string Codigo,
            string NombreAnt, string Nombre, decimal PrecioVenta, int Stock, string Descripcion, string Imagen)
        {
            DArticulo Datos = new DArticulo();
            Articulo Obj = new Articulo();
            if (NombreAnt.Equals(Nombre))
            {
                //Ingresa si el usuario solo cambia la descripción
                Obj.IdArticulo = Id;
                Obj.IdCategoria = IdCategoria;
                Obj.Codigo = Codigo;
                Obj.Nombre = Nombre;
                Obj.PrecioVenta = PrecioVenta;
                Obj.Stock = Stock;
                Obj.Descripcion = Descripcion;
                Obj.Imagen = Imagen;
                return Datos.Actualizar(Obj);
            }
            else
            {
                //validar que no exista una categoria con el mismo nombre
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1"))
                {
                    return "El artículo ya existe";
                }
                else
                {
                    Obj.IdArticulo = Id;
                    Obj.IdCategoria = IdCategoria;
                    Obj.Codigo = Codigo;
                    Obj.Nombre = Nombre;
                    Obj.PrecioVenta = PrecioVenta;
                    Obj.Stock = Stock;
                    Obj.Descripcion = Descripcion;
                    Obj.Imagen = Imagen;
                    return Datos.Actualizar(Obj);
                }
            }
        }

        public static string Eliminar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Eliminar(Id);
        }

        public static string Activar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Activar(Id);
        }

        public static string Desactivar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Desactivar(Id);
        }
    }
}
