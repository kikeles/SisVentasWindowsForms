using System;
using System.Data;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema.Negocio
{
    public class NCategoria
    {
        public static DataTable Listar()
        {
            DCategoria Datos = new DCategoria();
            return Datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Buscar(valor);
        }

        public static DataTable Seleccionar()
        {
            DCategoria Datos = new DCategoria();
            return Datos.Seleccionar();
        }
        public static string Insertar(string Nombre, string Descripcion)
        {
            DCategoria Datos = new DCategoria();
            //validar si ya existe
            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals("1"))
            {
                return "La categoria ya existe";
            }
            else
            {
                //si pasa la validacion se crea la categoria
                Categoria Obj = new Categoria();
                Obj.Nombre = Nombre;
                Obj.Descripcion = Descripcion;
                return Datos.Insertar(Obj);
            }
        }
        
        public static string Actualizar(int Id, string NombreAnt, string Nombre, string Descripcion)
        {
            DCategoria Datos = new DCategoria();
            Categoria Obj = new Categoria();
            if (NombreAnt.Equals(Nombre))
            {
                //Ingresa si el usuario solo cambia la descripción
                Obj.IdCategoria = Id;
                Obj.Nombre = Nombre;
                Obj.Descripcion = Descripcion;
                return Datos.Actualizar(Obj);
            }
            else
            {
                //validar que no exista una categoria con el mismo nombre
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1"))
                {
                    return "La categoria ya existe";
                }
                else
                {
                    Obj.IdCategoria = Id;
                    Obj.Nombre = Nombre;
                    Obj.Descripcion = Descripcion;
                    return Datos.Actualizar(Obj);
                }
            }
        }

        public static string Eliminar(int Id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Eliminar(Id);
        }

        public static string Activar(int Id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Activar(Id);
        }

        public static string Desactivar(int Id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Desactivar(Id);
        }
    }
}
