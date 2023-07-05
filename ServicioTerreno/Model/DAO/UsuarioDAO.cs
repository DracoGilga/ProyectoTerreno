using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioTerreno.Model.DAO
{
    public class UsuarioDAO
    {
        public static Boolean RegistrarUsuario(Usuario usuario)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                DBConexion.Usuario.InsertOnSubmit(usuario);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean ModificarUsuario(Usuario usuario)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Usuario usuarioModificar = DBConexion.Usuario.Where(p => p.IdUsuario == usuario.IdUsuario).First();
                usuarioModificar.Usuario1 = usuario.Usuario1;
                usuarioModificar.Contraseña = usuario.Contraseña;

                DBConexion.SubmitChanges();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public static Boolean EliminarUsuario(int idUsuario)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Usuario usuarioEliminar = DBConexion.Usuario.Where(p => p.IdUsuario == idUsuario).First();
                DBConexion.Usuario.DeleteOnSubmit(usuarioEliminar);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //login y configuracion de la conexion
        public static Usuario Login(string usuario, string contraseña)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Usuario usuarioLogin = DBConexion.Usuario.Where(p => p.Usuario1 == usuario && p.Contraseña == contraseña).First();
                return usuarioLogin;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataClassesTerrenosDataContext GetConexion()
        {
            return new DataClassesTerrenosDataContext(global::System.Configuration.
                ConfigurationManager.ConnectionStrings["TerrenosConnectionString"].ConnectionString);
        }
    }
}