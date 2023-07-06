using System;
using System.Linq;

namespace ServicioTerreno.Model.DAO
{
    public class UsuarioDAO
    {

        //login y configuracion de la conexion
        public static Usuario Login(string usuario, string contraseña)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Usuario usuarioLogin = DBConexion.Usuario.Where(p => p.Usuario1 == usuario && p.Contraseña == contraseña).First();
                if(usuarioLogin != null)
                {
                    return new Usuario()
                    {
                        IdUsuario = usuarioLogin.IdUsuario,
                        Usuario1 = usuarioLogin.Usuario1,
                        Contraseña = usuarioLogin.Contraseña,
                        IdPersona = usuarioLogin.IdPersona,
                    };
                }
                else
                {
                    return null;
                }
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