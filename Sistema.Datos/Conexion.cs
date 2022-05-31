using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;

namespace Sistema.Datos
{
    public class Conexion
    {
        //Solo podra usarse en esta clase
        private string Base;
        private string Servidor;
        private string Usuaro;
        private string Clave;
        private bool Seguridad;//ya sea windows o sql
        //Objeto que podrá compartir la cadena de Conexion
        private static Conexion Con = null;

        //No puede ser instanciada desde otra clase
        private Conexion()
        {
            this.Base = "dbsistemaprod";
            this.Servidor = "LUIS-ENRIQUE";
            this.Usuaro = "";
            this.Clave = "";
            this.Seguridad = true;//indica con que tipo de seguridad se establece la conexión
        }

        //SqlConnection usa la biblioteca using System.Data.SqlClient;
        public SqlConnection CrearConexion()
        {
            //https://www.connectionstrings.com/sql-server/
            SqlConnection Cadena = new SqlConnection();
            try
            {
                Cadena.ConnectionString = "Server="+this.Servidor+ "; Database="+this.Base+";";
                if (this.Seguridad)
                {
                    /*Ingresa cuando se trabaja con la seguridad de windows 
                     * Cadena de conexion:
                     * Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;
                     * Usar de preferencia cuando se trabaja con SQL Server:
                     * Integrated Security = SSPI (es para base de datos SQL Server)
                     */
                    Cadena.ConnectionString = Cadena.ConnectionString + "Integrated Security = SSPI";
                }
                else
                {
                    /*Ingresa cuando se trabaja con la seguridad de SQL Server
                     *Cadena de conexión:
                     *Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;
                     */
                    Cadena.ConnectionString = Cadena.ConnectionString + "User Id="+this.Usuaro+"; Password="+this.Clave;
                }
            }
            catch(Exception ex)
            {
                //Si hay un error la cadena sera null
                Cadena = null;
                throw ex;
            }
            return Cadena;
        }

        public static Conexion getInstancia()
        {
            //verificar si hay una instancia
            if(Con == null)
            {
                //si no hay una instancia crea una instancia
                Con = new Conexion();
            }
            return Con;
        }
    }
}
